using System.Collections.Specialized;
using System.Web;
using NotesFEService.Data.DTO;
using NotesFEService.Data.Models;

namespace NotesFEService.Data.ApiClient
{
    public class UserApiClient : IUserApiClient
    {
        private readonly string url;

        private readonly HttpClient client = new HttpClient();

        public UserApiClient(string url)
        {
            this.url = url;
        }

        public async Task<User?> GetUser(string id)
        {
            UriBuilder builder = new UriBuilder(url);
            NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);
            query["id"] = id;
            builder.Query = query.ToString();
            Uri uri = builder.Uri;
            HttpResponseMessage response = await client.GetAsync(uri);
            if(response.StatusCode != System.Net.HttpStatusCode.OK) throw new Exception($"API service {uri} returned status code {(int)response.StatusCode}.");
            UserNullable user = await response.Content.ReadFromJsonAsync<UserNullable>();
            if(user.Id != null) return new User()
            {
                Id = new Guid(user.Id.ToString()!),
                Email = user.Email!,
                Login = user.Login!
            };
            else return null;
        }
    }
}