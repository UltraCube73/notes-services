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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().Property(x => x.Login).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.Email).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.PasswordHash).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.PasswordSalt).IsRequired();
            modelBuilder.Entity<User>().HasIndex(x => x.Login);
            modelBuilder.Entity<User>().HasIndex(x => x.Email);
        }

        public DbSet<User> Users { get; set; }
    }
}