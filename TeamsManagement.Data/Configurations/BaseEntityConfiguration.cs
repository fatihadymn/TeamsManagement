using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamsManagement.Items.Entities;

namespace TeamsManagement.Data.Configurations
{
    public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(x => x.Id)
                .HasColumnType("uuid")
                .IsRequired();

            builder.Property(x => x.CreatedOn)
                   .HasColumnType("timestamp")
                   .IsRequired();

            builder.Property(x => x.UpdatedOn)
                   .HasColumnType("timestamp")
                   .IsRequired(false);
        }
    }
}
