using UserService.Data.Model;

namespace UserService.Repositoroes
{
    public interface IUserRepository
    {
        public User? GetById(Guid id);
        public User? GetByLogin(string login);
        public User? GetByEmail(string email);
        public void Create(User user);
        public void Update(User user);
        public void Delete(User user);
    }
}