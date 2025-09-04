using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection;
using UserService.Data.Model;

namespace UserService.Data
{
    public class UserDbContext : DbContext
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;

        public UserDbContext(DbContextOptions<UserDbContext> options, IDataProtectionProvider dataProtectionProvider) : base(options)
        {
            _dataProtectionProvider = dataProtectionProvider;
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(model =>
            {
                model.Property(x => x.Nickname).HasColumnType("bytea").HasConversion(new EncryptedConverter(_dataProtectionProvider));
                model.Property(x => x.Email).HasColumnType("bytea").HasConversion(new EncryptedConverter(_dataProtectionProvider));
            });
        }
    }
}