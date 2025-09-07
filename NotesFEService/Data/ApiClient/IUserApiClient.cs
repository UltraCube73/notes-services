using NotesFEService.Data.Models;

namespace NotesFEService.Data.ApiClient
{
    public interface IUserApiClient
    {
        public Task<User?> GetUser(string id);
    }
}