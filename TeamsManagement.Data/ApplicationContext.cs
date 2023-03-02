using Microsoft.EntityFrameworkCore;
using TeamsManagement.Items.Entities;

namespace TeamsManagement.Data
{
    public class ApplicationContext : DbContext
    {
        private readonly string schema = "TeamsManagement";

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(schema);

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataIdentifier).Assembly);
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
    }
}
