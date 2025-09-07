using NotesService.Data.Model;

namespace NotesService.Data.Repositories
{
    public interface ICategoryRepository
    {
        public Category GetById(string id);
        public Category GetByUser(string id);
        public void Create(Category category);
        public void Delete(Category category);
    }
}