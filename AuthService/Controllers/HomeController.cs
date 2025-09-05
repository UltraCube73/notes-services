using System.Diagnostics;
using AuthService.Data;
using AuthService.Data.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IApiClient _apiClient;

    public HomeController(ILogger<HomeController> logger, IApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new Data.Models.Views.Index() {});
    }

    [HttpPost]
    public IActionResult Index(Data.Models.Views.Index data)
    {
        data.Message = "Not implemented yet";
        return View(data);
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View(new Data.Models.Views.Register() {});
    }

    [HttpPost]
    public async Task<IActionResult> Register(Data.Models.Views.Register data)
    {
        if(data.Password != data.PasswordRepeat)
        {
            data.Message = "Пароли не совпадают!";
            return View(data);
        }
        ApiQueryResult result = await _apiClient.Register(new UserRegistrationInfo() { Email = data.Email, Password = data.Password, Nickname = data.Login });
        if(result.isSuccessful) data.Message = "Успешно";
        else data.Message = "Фигово";
        return View(data);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new Data.Models.Views.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
