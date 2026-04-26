using Microsoft.EntityFrameworkCore;
using SecureFileStorage.Models;
using Microsoft.EntityFrameworkCore.Design;


namespace SecureFileStorage.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<StoredFile> Files { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StoredFile>(builder =>
            {
                builder.HasKey(x => x.Id);

                builder.Property(x => x.OriginalName).IsRequired();
                builder.Property(x => x.StoredName).IsRequired();
                builder.Property(x => x.ContentType).IsRequired();
                builder.Property(x => x.StoragePath).IsRequired();
            });
        }
    }
}