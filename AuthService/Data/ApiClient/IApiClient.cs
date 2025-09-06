using AuthService.Data.DTO;

namespace AuthService.Data
{
    public interface IApiClient
    {
        public Task<ApiQueryResult> CheckIfUserExists(UserEmailLoginInfo user);
        public Task<ApiQueryResult> Login(UserLoginInfo user);
        public Task<ApiQueryResult> Register(UserRegistrationInfo user);
    }
}