using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamsManagement.Items.Entities;

namespace TeamsManagement.Data.Configurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.Property(x => x.Name)
                   .HasColumnName("name")
                   .HasColumnType("varchar")
                   .IsRequired();

            builder.Property(x => x.Country)
                   .HasColumnType("varchar")
                   .HasColumnName("country")
                   .IsRequired();
        }
    }
}
