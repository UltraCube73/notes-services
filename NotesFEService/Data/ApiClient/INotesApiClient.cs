using NotesFEService.Data.Models;

namespace NotesFEService.Data.ApiClient
{
    public interface INotesApiClient
    {
        public Task<List<Category>> GetCategories(string userId);
        public Task CreateCategory(Category category);
        public Task DeleteCategory(string id);

        public Task<List<Note>> GetNotes(string categoryId);
    }
}