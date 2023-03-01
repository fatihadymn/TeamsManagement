using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamsManagement.Items.Entities;

namespace TeamsManagement.Data.Configurations
{
    public class BaseEntityConfiguration : IEntityTypeConfiguration<BaseEntity>
    {
        public void Configure(EntityTypeBuilder<BaseEntity> builder)
        {
            builder.Property(x => x.Id)
                .HasColumnType("uuid")
                .HasColumnName("id")
                .IsRequired();

            builder.Property(x => x.CreatedOn)
                   .HasColumnName("created_on")
                   .HasColumnType("timestamp")
                   .IsRequired();

            builder.Property(x => x.UpdatedOn)
                   .HasColumnName("updated_on")
                   .HasColumnType("timestamp")
                   .IsRequired(false);
        }
    }
}
