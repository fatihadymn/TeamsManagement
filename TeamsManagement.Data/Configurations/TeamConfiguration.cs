using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamsManagement.Items.Entities;

namespace TeamsManagement.Data.Configurations
{
    public class TeamConfiguration : BaseEntityConfiguration<Team>
    {
        public override void Configure(EntityTypeBuilder<Team> builder)
        {
            base.Configure(builder);

            builder.ToTable("Teams");

            builder.Property(x => x.Name)
                   .HasColumnType("varchar")
                   .IsRequired();

            builder.Property(x => x.Country)
                   .HasColumnType("varchar")
                   .IsRequired();
        }
    }
}
