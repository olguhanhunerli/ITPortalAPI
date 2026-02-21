using ITPortal.Entities.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Business.EntityConfigurations
{
    public class LookupTypeConfiguration : IEntityTypeConfiguration<LookupType>
    {
        public void Configure(EntityTypeBuilder<LookupType> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code)
                   .IsRequired()
                   .HasMaxLength(60);
            builder.Property(x => x.NameTr)
                   .IsRequired()
                   .HasMaxLength(120);
            builder.Property(x => x.NameEn)
                   .IsRequired()
                   .HasMaxLength(120);
            builder.HasIndex(x => x.Code)
                .IsUnique();

            builder.HasMany(x => x.Lookups)
             .WithOne(x => x.LookupType)
             .HasForeignKey(x => x.LookupTypeId);

            builder.Property(x => x.CreatedAt).IsRequired();
        }
    }
}
