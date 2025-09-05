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

        public async Task<ApiQueryResult> CheckIfUserExists(User user)
        {
            //HttpResponseMessage response = await client.GetAsync(new Uri(url, ""));
            return new ApiQueryResult(true);
        }

        public async Task<ApiQueryResult> Login(User user)
        {
            return new ApiQueryResult(true);
        }

        public async Task<ApiQueryResult> Register(UserRegistrationInfo user)
        {
            HttpResponseMessage response;
            response = await client.PostAsJsonAsync(new Uri(new Uri(url), "/register"), user);
            if(response.StatusCode == System.Net.HttpStatusCode.OK) return new ApiQueryResult(true);
            else throw new Exception($"API service {url} returned non-200 status code");
        }
    }
}