using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NotesFEService.Data.ApiClient;
using NotesFEService.Data.Models;

namespace NotesFEService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IUserApiClient _userapi;

        private readonly INotesApiClient _notesapi;

        public HomeController(ILogger<HomeController> logger, IUserApiClient userapi, INotesApiClient notesapi)
        {
            _logger = logger;
            _userapi = userapi;
            _notesapi = notesapi;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index(Data.Models.Views.Index? data, string? CurrentCategory)
        {
            User? user = await _userapi.GetUser(User.Identity.Name);
            if(user == null) return Unauthorized();

            CategoryInfo categoryInfo = await GenerateSelectList(user.Id);
            if(data == null) data = new Data.Models.Views.Index() { User = user, Options = categoryInfo.Options, HasOptions = categoryInfo.HasOptions };
            else
            {
                data.User = user;
                data.Options = categoryInfo.Options;
                data.HasOptions = categoryInfo.HasOptions;
            }
            if(CurrentCategory != null) data.CurrentCategory = CurrentCategory;
            else
            {
                data.CurrentCategory = categoryInfo.Options[0].Value;
                RouteData.Values.Add("CurrentCategory", data.CurrentCategory);
            }

            if(data.HasOptions) data.Notes = await _notesapi.GetNotes(data.CurrentCategory);
            return View(data);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Index(Data.Models.Views.Index data)
        {
            User? user = await _userapi.GetUser(User.Identity.Name);
            if(user == null) return Unauthorized();

            if (data.NewCategoryName == null || data.NewCategoryName.Trim() == "" || data.NewCategoryName.Count() >= 15)
            {
                if (data.NewCategoryName == null || data.NewCategoryName.Trim() == "") data.StatusMessage = "В названии категории должен быть текст";
                else data.StatusMessage = "Максимальная длина названия категории - 15 символов";
                return View(data);
            }

            Category category = new Category() { Name = data.NewCategoryName, OwnerId = user.Id };

            await _notesapi.CreateCategory(category);

            CategoryInfo categoryInfo = await GenerateSelectList(user.Id);
            data.User = user;
            data.Options = categoryInfo.Options;
            data.HasOptions = categoryInfo.HasOptions;

            if(data.HasOptions) data.Notes = await _notesapi.GetNotes(categoryInfo.Options[0].Value);

            return View(data);
        }

        public async Task<IActionResult> Delete(Data.Models.Views.Index data, string id)
        {
            User? user = await _userapi.GetUser(User.Identity.Name);
            if(user == null) return Unauthorized();

            Category? category = (await _notesapi.GetCategories(user.Id.ToString())).Where(x => x.Id == new Guid(id)).FirstOrDefault();

            if(category != null) await _notesapi.DeleteCategory(id);

            CategoryInfo categoryInfo = await GenerateSelectList(user.Id);
            data.User = user;
            data.Options = categoryInfo.Options;
            data.HasOptions = categoryInfo.HasOptions;
            data.CurrentCategory = categoryInfo.Options[0].Value;

            if(data.HasOptions) data.Notes = await _notesapi.GetNotes(data.CurrentCategory);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new Data.Models.Views.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private class CategoryInfo()
        {
            public bool HasOptions { get; set; }
            public List<SelectListItem> Options;
        }

        private async Task<CategoryInfo> GenerateSelectList(Guid id)
        {
            List<SelectListItem> options = (await _notesapi.GetCategories(id.ToString())).Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();
            if(options.Count == 0)
            {
                options.Add(new SelectListItem() { Value = null, Text = "Создайте категорию..." });
                return new CategoryInfo() { Options = options, HasOptions = false };
            }
            else return new CategoryInfo() { Options = options, HasOptions = true };
        }
    }
}
