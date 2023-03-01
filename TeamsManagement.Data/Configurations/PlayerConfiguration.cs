using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamsManagement.Items.Entities;

namespace TeamsManagement.Data.Configurations
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.Property(x => x.Name)
                   .HasColumnName("name")
                   .HasColumnType("varchar")
                   .IsRequired();

            builder.Property(x => x.Height)
                   .HasColumnName("height")
                   .IsRequired();

            builder.Property(x => x.DateOfBirth)
                   .HasColumnType("timestamp")
                   .HasColumnName("date_of_birth")
                   .IsRequired();

            builder.HasOne(x => x.Team)
                   .WithMany(x => x.Players)
                   .HasForeignKey(x => x.TeamId);
        }
    }
}
