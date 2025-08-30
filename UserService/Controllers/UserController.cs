using Microsoft.AspNetCore.Mvc;
using UserService.Data.DTO;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public UserInfo Get(string? id, string? email)
        {
            return new UserInfo() { Id = new Guid(), Email = "example@abc.xyz" };
        }
    }
}