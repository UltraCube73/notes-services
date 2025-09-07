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

        public async Task<UserExistenceInfo> CheckIfUserExists(UserEmailLoginInfo user)
        {
            Uri uri = new Uri(new Uri(url), "check");
            HttpResponseMessage response = await client.PostAsJsonAsync(uri, user);
            if(response.StatusCode != System.Net.HttpStatusCode.OK) throw new Exception($"API service {uri} returned status code {(int)response.StatusCode}.");
            return await response.Content.ReadFromJsonAsync<UserExistenceInfo>();
        }

        public async Task<UserLoginResultInfo> Login(UserSigninInfo user)
        {
            Uri uri = new Uri(new Uri(url), "login");
            HttpResponseMessage response = await client.PostAsJsonAsync(uri, user);
            if(response.StatusCode != System.Net.HttpStatusCode.OK) throw new Exception($"API service {uri} returned status code {(int)response.StatusCode}.");
            return await response.Content.ReadFromJsonAsync<UserLoginResultInfo>();
        }

        public async Task<UserLoginResultInfo> Register(UserRegistrationInfo user)
        {
            Uri uri = new Uri(new Uri(url), "register");
            HttpResponseMessage response = await client.PostAsJsonAsync(uri, user);
            if(response.StatusCode != System.Net.HttpStatusCode.OK) throw new Exception($"API service {uri} returned status code {((int)response.StatusCode)}.");
            return await response.Content.ReadFromJsonAsync<UserLoginResultInfo>();
        }
    }
}