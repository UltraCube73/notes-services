using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesFEService.Data.ApiClient;
using NotesFEService.Data.DTO;
using NotesFEService.Data.Models;
using NotesFEService.Data.Models.Views;

namespace NotesFEService.Controllers
{
    [Route("[controller]")]
    public class EditController : Controller
    {
        private readonly ILogger<EditController> _logger;

        private readonly IUserApiClient _userapi;

        private readonly INotesApiClient _notesapi;

        public EditController(ILogger<EditController> logger, IUserApiClient userapi, INotesApiClient notesapi)
        {
            _logger = logger;
            _userapi = userapi;
            _notesapi = notesapi;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index(Edit? data, string? categoryId, string? noteId)
        {
            User? user = await _userapi.GetUser(User.Identity.Name);
            if(user == null) return Unauthorized();
            if(categoryId == null) return Redirect("/");
            Category? category = (await _notesapi.GetCategories(user.Id.ToString())).Where(x => x.Id == new Guid(categoryId)).FirstOrDefault();
            if(category == null) return Unauthorized();

            if(data == null) data = new Edit() { };

            if (noteId != null)
            {
                Note note = (await _notesapi.GetNotes(categoryId)).Where(x => x.Id == new Guid(noteId)).FirstOrDefault();
                data.NoteId = note.Id.ToString();
                data.NoteText = note.Text;
                data.IsExisting = true;
            }

            data.categoryId = categoryId;
            return View(data);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Index(Edit data, string categoryId)
        {
            User? user = await _userapi.GetUser(User.Identity.Name);
            if(user == null) return Unauthorized();
            if(categoryId == null) return Redirect("/");
            Category? category = (await _notesapi.GetCategories(user.Id.ToString())).Where(x => x.Id == new Guid(categoryId)).FirstOrDefault();
            if(category == null) return Unauthorized();

            Note? note = null;
            if(data.NoteId != null) note = (await _notesapi.GetNotes(categoryId)).Where(x => x.Id == new Guid(data.NoteId)).FirstOrDefault();
            if(note == null) await _notesapi.CreateNote(new NoteCreationInfo() { Text = data.NoteText, CategoryId = category.Id });
            else await _notesapi.UpdateNote(new Note() { Id = new Guid(data.NoteId), Text = data.NoteText, CategoryId = category.Id });

            return RedirectToAction("Index", "Home", new { CurrentCategory = categoryId });
        }

        [Authorize]
        [HttpGet("delete")]
        public async Task<IActionResult> Delete(Edit data, string? categoryId, string? noteId)
        {
            User? user = await _userapi.GetUser(User.Identity.Name);
            if(user == null) return Unauthorized();
            if(categoryId == null) return Redirect("/");
            Category? category = (await _notesapi.GetCategories(user.Id.ToString())).Where(x => x.Id == new Guid(categoryId)).FirstOrDefault();
            if(category == null) return Unauthorized();

            Note? note = (await _notesapi.GetNotes(categoryId)).Where(x => x.Id == new Guid(noteId)).FirstOrDefault();
            if(note == null) return Unauthorized();

            await _notesapi.DeleteCategory(note.Id.ToString());

            return RedirectToAction("Index", "Home", new { CurrentCategory = categoryId });
        }
    }
}