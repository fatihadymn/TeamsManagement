using Microsoft.EntityFrameworkCore;
using TeamsManagement.Items.Entities;

namespace TeamsManagement.Data
{
    public class ApplicationContext : DbContext
    {
        private readonly string schema = "TeamsManagement";
        private readonly Action<ApplicationContext, ModelBuilder> _modelCustomizer;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options, Action<ApplicationContext, ModelBuilder> modelCustomizer = null)
        : base(options)
        {
            _modelCustomizer = modelCustomizer;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(schema);

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataIdentifier).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(
                    @"host=localhost;port=56002;database=TeamsManagement.InMemory;username=admin;password=admin");
            }
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
    }
}
