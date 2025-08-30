using UserService.Data.Model;

namespace UserService.Repositoroes
{
    public interface IUserRepository
    {
        public User GetById(Guid id);
        public void Create(User user);
        public void Update(User user);
        public void Delete(User user);
    }
}