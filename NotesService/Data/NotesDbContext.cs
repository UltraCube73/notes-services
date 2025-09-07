using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using NotesService.Data.Model;

namespace NotesService.Data
{
    public class NotesDbContext : DbContext
    {
        private readonly IDataProtectionProvider dataProtectionProvider;

        public NotesDbContext(DbContextOptions<NotesDbContext> options, IDataProtectionProvider dataProtectionProvider) : base(options)
        {
            this.dataProtectionProvider = dataProtectionProvider;
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasKey(x => x.Id);
            modelBuilder.Entity<Category>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Category>().Property(x => x.OwnerId).IsRequired();
            modelBuilder.Entity<Category>().HasIndex(x => x.Name);

            modelBuilder.Entity<Note>().HasKey(x => x.Id);
            modelBuilder.Entity<Note>().Property(x => x.Category).IsRequired();
            modelBuilder.Entity<Note>().Property(x => x.Text).IsRequired();

            modelBuilder.Entity<Category>(model =>
            {
                model.Property(x => x.Name).HasColumnType("bytea").HasConversion(new EncryptedConverter(dataProtectionProvider));
            });

            modelBuilder.Entity<Note>(model =>
            {
                model.Property(x => x.Text).HasColumnType("bytea").HasConversion(new EncryptedConverter(dataProtectionProvider));
            });
        }
    }
}