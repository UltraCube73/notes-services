using AuthService.Data.DTO;

namespace AuthService.Data
{
    public interface IApiClient
    {
        public Task<UserExistenceInfo> CheckIfUserExists(UserEmailLoginInfo user);
        public Task<UserLoginResultInfo> Login(UserSigninInfo user);
        public Task<UserLoginResultInfo> Register(UserRegistrationInfo user);
    }
}