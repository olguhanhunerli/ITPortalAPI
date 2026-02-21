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
    public class LookupConfiguration : IEntityTypeConfiguration<Lookup>
    {
        public void Configure(EntityTypeBuilder<Lookup> b)
        {
            b.HasKey(x => x.Id);

            b.Property(x => x.Code).HasMaxLength(80).IsRequired();
            b.Property(x => x.NameTr).HasMaxLength(160).IsRequired();
            b.Property(x => x.NameEn).HasMaxLength(160).IsRequired();
            b.Property(x => x.DescriptionTr).HasMaxLength(600);
            b.Property(x => x.DescriptionEn).HasMaxLength(600);

            b.Property(x => x.SortOrder);
            b.Property(x => x.IsActive);

            b.HasIndex(x => new { x.LookupTypeId, x.Code }).IsUnique();
            b.HasIndex(x => new { x.LookupTypeId, x.SortOrder });

            b.Property(x => x.CreatedAt).IsRequired();
            b.Property(x => x.UpdatedAt).IsRequired();
        }
    }
}
