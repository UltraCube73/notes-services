using AuthService.Data.DTO;

namespace AuthService.Data
{
    public interface IApiClient
    {
        public Task<UserExistenceInfo> CheckIfUserExists(UserEmailLoginInfo user);
        public Task<ApiQueryResult> Login(UserLoginInfo user);
        public Task Register(UserRegistrationInfo user);
    }
}