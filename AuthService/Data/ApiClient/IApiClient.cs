using AuthService.Data.DTO;

namespace AuthService.Data
{
    public interface IApiClient
    {
        public Task<ApiQueryResult> CheckIfUserExists(User user);
        public Task<ApiQueryResult> Login(User user);
        public Task<ApiQueryResult> Register(UserRegistrationInfo user);
    }
}