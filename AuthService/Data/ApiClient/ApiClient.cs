using AuthService.Data.DTO;

namespace AuthService.Data
{
    public class ApiClient : IApiClient
    {
        private readonly string url;

        private readonly HttpClient client = new HttpClient();

        public ApiClient(string apiUrl)
        {
            url = apiUrl;
        }

        public ApiQueryResult CheckIfUserExists(User user)
        {
            return new ApiQueryResult(true, "ok");
        }

        public ApiQueryResult Login(User user)
        {
            return new ApiQueryResult(true, "ok");
        }

        public ApiQueryResult Register(User user)
        {
            return new ApiQueryResult(true, "ok");
        }
    }
}