using Microsoft.EntityFrameworkCore;
using NotesService.Data.Model;

namespace NotesService.Data.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly NotesDbContext dbContext;

        public NoteRepository(NotesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Note GetById(string id)
        {
            return dbContext.Notes.Where(x => x.Id == new Guid(id)).FirstOrDefault()!;
        }

        public List<Note> GetByCategory(string categoryId)
        {
            return dbContext.Categories.Include(x => x.Notes).Where(x => x.Id == new Guid(categoryId)).FirstOrDefault()!.Notes;
        }

        public void Create(Note note)
        {
            dbContext.Notes.Add(note);
            dbContext.SaveChanges();
        }

        public void Update(Note note)
        {
            dbContext.Notes.Where(x => x.Id == note.Id).FirstOrDefault()!.Text = note.Text;
            dbContext.SaveChanges();
        }

        public void Delete(string id)
        {
            dbContext.Notes.Remove(dbContext.Notes.Where(x => x.Id == new Guid(id)).FirstOrDefault()!);
            dbContext.SaveChanges();
        }
    }
}