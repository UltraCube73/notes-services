using AuthService.Data.DTO;
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
                Login = user.Login,
                Email = user.Email
            };
        }

        [HttpPost("check")]
        public UserExistenceInfo Check(UserEmailLoginInfo info)
        {
            User? UserByEmail = _repository.GetByEmail(info.Email);
            User? UserByLogin = _repository.GetByLogin(info.Login);
            return new UserExistenceInfo()
            {
                LoginExists = UserByLogin != null,
                EmailExists = UserByEmail != null
            };
        }

        [HttpPost("register")]
        public IActionResult Register(UserRegistrationInfo info)
        {
            Hashing.HashResult hash = Hashing.Create(info.Password);
            User user = new User() {
                Login = info.Login,
                Email = info.Email,
                PasswordHash = hash.Hash,
                PasswordSalt = hash.Salt
            };
            _repository.Create(user);
            return Ok();
        }
    }
}