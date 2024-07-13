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

        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Status> Status { get; set; }

    }
}
