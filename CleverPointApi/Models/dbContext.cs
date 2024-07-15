using Microsoft.EntityFrameworkCore;

namespace CleverPointApi.Models
{
    public class CleverPointDbContext : DbContext
    {
        public CleverPointDbContext(DbContextOptions<CleverPointDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>()
                .HasIndex(t => t.ShipmentID)
                .IsUnique();

            modelBuilder.Entity<User>()
               .HasIndex(t => t.Username)
               .IsUnique();
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Status> Status { get; set; }

    }
}
