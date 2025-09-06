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
        if(data.Email == null || data.Login == null || data.Password == null || data.PasswordRepeat == null) data.Message = "Данные не заполнены!";
        if(data.Password != data.PasswordRepeat) data.Message = "Пароли не совпадают!";
        else
        {
            UserExistenceInfo userExistenceInfo = await _apiClient.CheckIfUserExists(new UserEmailLoginInfo() { Email = data.Email!, Login = data.Login! });
            if(userExistenceInfo.LoginExists) data.Message = "Логин занят!";
            if(userExistenceInfo.EmailExists) data.Message = "Почта уже зарегистрирована!";
            else
            {
                UserLoginResultInfo info = await _apiClient.Register(new UserRegistrationInfo() { Email = data.Email!, Password = data.Password!, Login = data.Login! });
                data.Message = "Регистрация прошла успешно.";
                Response.Cookies.Append("X-Access-Token", info.Token!, new CookieOptions() { SameSite = SameSiteMode.Strict });
                return View(data);
            }
            return View(data);
        }
        return View(data);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new Data.Models.Views.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
