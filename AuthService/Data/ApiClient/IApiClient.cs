using AuthService.Data.DTO;

namespace AuthService.Data
{
    public interface IApiClient
    {
        public ApiQueryResult CheckIfUserExists(User user);
        public ApiQueryResult Login(User user);
        public ApiQueryResult Register(User user);
    }
}