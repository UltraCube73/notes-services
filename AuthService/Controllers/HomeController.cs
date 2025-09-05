using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AuthService.Data;

namespace AuthService.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(Data.Models.Views.Index? data)
    {
        if(data == null) return View(new Data.Models.Views.Index());
        else return View(new Data.Models.Views.Index() { Message = "post" });
    }

    public IActionResult Register()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new Data.Models.Views.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
