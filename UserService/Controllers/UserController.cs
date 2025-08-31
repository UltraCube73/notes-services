using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UserService.Data;
using UserService.Data.DTO;
using UserService.Data.Model;
using UserService.Repositoroes;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _repository;

        public UserController(UserDbContext dbContext)
        {
            _repository = new UserRepository(dbContext);
        }

        [HttpGet]
        public UserInfo Get(string id)
        {
            User user = _repository.GetById(Guid.Parse(id));
            return new UserInfo() {
                Id = user.Id,
                Nickname = user.Nickname,
                Email = user.Email
            };
        }

        [HttpPost("register")]
        public IActionResult Register(UserRegistrationInfo info)
        {
            Hashing.HashResult hash = Hashing.Create(info.Password);
            User user = new User() {
                Nickname = info.Nickname,
                Email = info.Email,
                PasswordHash = hash.Hash,
                PasswordSalt = hash.Salt
            };
            _repository.Create(user);
            return Ok();
        }
    }
}