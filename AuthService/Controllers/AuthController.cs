using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
  public class AuthController : Controller
  {
    public IActionResult login()
    {
      return Ok();
    }
  }
}