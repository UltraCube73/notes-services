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
            Uri uri = new Uri(new Uri(url), "register");
            HttpResponseMessage response;
            response = await client.PostAsJsonAsync(uri, user);
            if(response.StatusCode == System.Net.HttpStatusCode.OK) return new ApiQueryResult(true);
            else throw new Exception($"API service {uri} returned status code {((int)response.StatusCode)}.");
        }
    }
}