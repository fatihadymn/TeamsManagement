using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamsManagement.Items.Entities;

namespace TeamsManagement.Data.Configurations
{
    public class PlayerConfiguration : BaseEntityConfiguration<Player>
    {
        public override void Configure(EntityTypeBuilder<Player> builder)
        {
            base.Configure(builder);

            builder.ToTable("Players");

            builder.Property(x => x.Name)
                   .HasColumnType("varchar")
                   .IsRequired();

            builder.Property(x => x.Height)
                   .IsRequired();

            builder.Property(x => x.DateOfBirth)
                   .HasColumnType("timestamp")
                   .IsRequired();

            builder.HasOne(x => x.Team)
                   .WithMany(x => x.Players)
                   .HasForeignKey(x => x.TeamId)
                   .IsRequired(false);
        }
    }
}
