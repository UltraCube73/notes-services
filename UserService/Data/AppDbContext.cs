using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection;

namespace UserService
{
    public class AppDbContext : DbContext
    {
        private IDataProtectionProvider dataProtectionProvider;

        public AppDbContext(DbContextOptions<AppDbContext> options, IDataProtectionProvider dataProtectionProvider) : base(options)
        {
            this.dataProtectionProvider = dataProtectionProvider;
            Database.EnsureCreated();
        }
    }
}