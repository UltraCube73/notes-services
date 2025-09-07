using System.Collections.Specialized;
using System.Web;
using NotesFEService.Data.DTO;
using NotesFEService.Data.Models;

namespace NotesFEService.Data.ApiClient
{
    public class NotesApiClient : INotesApiClient
    {
        private readonly string url;

        private readonly HttpClient client = new HttpClient();

        public NotesApiClient(string url)
        {
            this.url = url;
        }

        public async Task<List<Category>> GetCategories(string userId)
        {
            UriBuilder builder = new UriBuilder(new Uri(new Uri(url), "Category/by-user"));
            NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);
            query["id"] = userId;
            builder.Query = query.ToString();
            Uri uri = builder.Uri;
            HttpResponseMessage response = await client.GetAsync(uri);
            if(response.StatusCode != System.Net.HttpStatusCode.OK) throw new Exception($"API service {uri} returned status code {(int)response.StatusCode}.");
            return await response.Content.ReadFromJsonAsync<List<Category>>();
        }

        public async Task CreateCategory(Category category)
        {
            Uri uri = new Uri(new Uri(url), "Category/create");
            HttpResponseMessage response = await client.PostAsJsonAsync(uri, category);
            if(response.StatusCode != System.Net.HttpStatusCode.OK) throw new Exception($"API service {uri} returned status code {(int)response.StatusCode}.");
        }

        public async Task DeleteCategory(string id)
        {
            Uri uri = new Uri(new Uri(url), "Category/delete");
            HttpResponseMessage response = await client.PostAsJsonAsync(uri, new CategoryId() { id = id });
            if(response.StatusCode != System.Net.HttpStatusCode.OK) throw new Exception($"API service {uri} returned status code {(int)response.StatusCode}.");
        }

        public async Task<List<Note>> GetNotes(string categoryId)
        {
            UriBuilder builder = new UriBuilder(new Uri(new Uri(url), "Note/by-category"));
            NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);
            query["id"] = categoryId;
            builder.Query = query.ToString();
            Uri uri = builder.Uri;
            HttpResponseMessage response = await client.GetAsync(uri);
            if(response.StatusCode != System.Net.HttpStatusCode.OK) throw new Exception($"API service {uri} returned status code {(int)response.StatusCode}.");
            return await response.Content.ReadFromJsonAsync<List<Note>>();
        }
    }
}