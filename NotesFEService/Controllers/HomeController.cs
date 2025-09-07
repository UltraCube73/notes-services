using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesFEService.Data.ApiClient;

namespace NotesFEService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IUserApiClient _userapi;

        public HomeController(ILogger<HomeController> logger, IUserApiClient userapi)
        {
            _logger = logger;
            _userapi = userapi;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Data.Models.User? user = await _userapi.GetUser(User.Identity.Name);
            if(user == null) return Unauthorized();
            return View(new Data.Models.Views.Index() { HasOptions = false, User = user });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new Data.Models.Views.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
