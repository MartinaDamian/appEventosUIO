using Microsoft.EntityFrameworkCore;
using QuitoVibesMvc.Models;

namespace QuitoVibesMvc.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<EventoCultural> Eventos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mappings & Configurations
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Username).IsUnique();
                entity.HasIndex(u => u.Email).IsUnique();
            });

            modelBuilder.Entity<EventoCultural>(entity =>
            {
                entity.Property(e => e.Precio).HasColumnType("decimal(18,2)");
            });
        }
    }
}
