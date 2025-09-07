using NotesService.Data.Model;

namespace NotesService.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly NotesDbContext dbContext;

        public CategoryRepository(NotesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Category GetById(string id)
        {
            return dbContext.Categories.Where(x => x.Id == new Guid(id)).FirstOrDefault()!;
        }

        public List<Category> GetByUser(string id)
        {
            return dbContext.Categories.Where(x => x.OwnerId == new Guid(id)).ToList();
        }

        public void Create(Category category)
        {
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();
        }

        public void Delete(string id)
        {
            dbContext.Categories.Remove(dbContext.Categories.Where(x => x.Id == new Guid(id)).FirstOrDefault()!);
            dbContext.SaveChanges();
        }
    }
}