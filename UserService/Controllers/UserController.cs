using System.IdentityModel.Tokens.Jwt;
using AuthService.Data.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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

        private readonly JwtSigner _signer;

        public UserController(UserDbContext dbContext, JwtSigner signer, ILogger<UserController> logger)
        {
            _repository = new UserRepository(dbContext);
            _signer = signer;
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
            User? UserByEmail = null;
            User? UserByLogin = null;
            if(info.Email != null) UserByEmail = _repository.GetByEmail(info.Email);
            if(info.Login != null) UserByLogin = _repository.GetByLogin(info.Login);
            return new UserExistenceInfo()
            {
                LoginExists = UserByLogin != null,
                EmailExists = UserByEmail != null
            };
        }

        [HttpPost("register")]
        public UserLoginResultInfo Register(UserRegistrationInfo info)
        {
            Hashing.HashResult hash = Hashing.Create(info.Password);
            User user = new User() {
                Login = info.Login,
                Email = info.Email,
                PasswordHash = hash.Hash,
                PasswordSalt = hash.Salt
            };
            _repository.Create(user);
            return new UserLoginResultInfo() { IsSuccessful = true, Token = _signer.Create(user.Id) };
        }

        [HttpPost("login")]
        public UserLoginResultInfo Login(UserSigninInfo info)
        {
            User? user = _repository.GetByEmail(info.Email);
            if(user == null) return new UserLoginResultInfo() { IsSuccessful = false };
            bool isValid = Hashing.Verify(info.Password, user.PasswordHash, user.PasswordSalt);
            if(isValid)
            {
                return new UserLoginResultInfo() { IsSuccessful = true, Token = _signer.Create(user.Id) };
            }
            else return new UserLoginResultInfo() { IsSuccessful = false };
        }
    }
}