using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection;
using UserService.Data.Model;

namespace UserService.Data
{
    public class UserDbContext : DbContext
    {

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
    }
}