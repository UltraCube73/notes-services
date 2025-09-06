using System.Diagnostics;
using System.Net.Mail;
using System.Text.RegularExpressions;
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
    public async Task<IActionResult> Index(Data.Models.Views.Index data)
    {
        if(data.Email != null) data.Email = data.Email.Trim();
        if (data.Email == null || data.Password == null || data.Email == "" || data.Password == "")
        {
            data.Message = "Данные не заполнены!";
            return View(data);
        }
        Regex mailRexex = new Regex(@"^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$");
        if(!mailRexex.IsMatch(data.Email))
        {
            data.Message = "Неверный формат почты!";
            return View(data);
        }

        UserExistenceInfo userExistenceInfo = await _apiClient.CheckIfUserExists(new UserEmailLoginInfo() { Email = data.Email! });
        if(!userExistenceInfo.EmailExists)
        {
            data.Message = "Пользователь с данным почтовым адресом не зарегистрирован!";
            return View(data);
        }

        UserLoginResultInfo info = await _apiClient.Login(new UserSigninInfo() { Email = data.Email, Password = data.Password });
        if(!info.IsSuccessful)
        {
            data.Message = "Неверный пароль!";
            return View(data);
        }

        Response.Cookies.Append("X-Access-Token", info.Token!, new CookieOptions() { SameSite = SameSiteMode.Strict });
        return Redirect("/");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View(new Data.Models.Views.Register() {});
    }

    [HttpPost]
    public async Task<IActionResult> Register(Data.Models.Views.Register data)
    {
        if(data.Email != null) data.Email = data.Email.Trim().ToLower();
        if(data.Login != null) data.Login = data.Login.Trim();
        if (data.Email == null || data.Login == null || data.Password == null || data.PasswordRepeat == null || data.Email == "" || data.Login == "")
        {
            data.Message = "Данные не заполнены!";
            return View(data);
        }
        Regex mailRexex = new Regex(@"^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$");
        if(!mailRexex.IsMatch(data.Email))
        {
            data.Message = "Неверный формат почты!";
            return View(data);
        }
        Regex loginRegex = new Regex(@"^[A-Za-z0-9]+$");
        if(!loginRegex.IsMatch(data.Login) || data.Login.Length < 5)
        {
            data.Message = "Никнейм может состоять из цифр и английских букв, минимальная длина - 5 символов!";
            return View(data);
        }
        if(data.Password.Length < 8)
        {
            data.Message = "Минимальная длина пароля - 8 символов!";
            return View(data);
        }
        if (data.Password != data.PasswordRepeat)
        {
            data.Message = "Пароли не совпадают!";
            return View(data);
        }

        UserExistenceInfo userExistenceInfo = await _apiClient.CheckIfUserExists(new UserEmailLoginInfo() { Email = data.Email!, Login = data.Login! });
        if (userExistenceInfo.LoginExists) data.Message = "Логин занят!";
        else if (userExistenceInfo.EmailExists) data.Message = "Почта уже зарегистрирована!";
        else
        {
            UserLoginResultInfo info = await _apiClient.Register(new UserRegistrationInfo() { Email = data.Email!, Password = data.Password!, Login = data.Login! });
            Response.Cookies.Append("X-Access-Token", info.Token!, new CookieOptions() { SameSite = SameSiteMode.Strict });
            return Redirect("/");
        }

        return View(data);
        
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new Data.Models.Views.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
