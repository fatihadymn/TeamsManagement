using Microsoft.EntityFrameworkCore;

namespace TeamsManagement.Data
{
    public class ApplicationContext : DbContext
    {
        private readonly string schema = "teamsManagement";

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(schema);

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataIdentifier).Assembly);
        }

        //public DbSet<DailyRate> DailyRates { get; set; }
    }
}
