using NotesService.Data.Model;

namespace NotesService.Data.Repositories
{
    public interface INoteRepository
    {
        public Note GetById(string id);
        public List<Note> GetByCategory(string categoryId);
        public void Create(Note note);
        public void Update(Note note);
        public void Delete(string id);
    }
}