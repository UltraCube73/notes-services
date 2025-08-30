using UserService.Data;
using UserService.Data.Model;

namespace UserService.Repositoroes
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;
        public UserRepository(UserDbContext context)
        {
            _context = context;
        }
        public User GetById(Guid id)
        {
            return _context.Users.Where(x => x.Id == id).FirstOrDefault()!;
        }
        public void Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public void Update(User user)
        {
            throw new NotImplementedException();
        }
        public void Delete(User user)
        {
            throw new NotImplementedException();
        }
    }
}