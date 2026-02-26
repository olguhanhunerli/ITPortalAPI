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
    public class BusinessHoursConfiguration : IEntityTypeConfiguration<BusinessHours>
    {
        public void Configure(EntityTypeBuilder<BusinessHours> b)
        {

            b.HasKey(x => x.Id);

            b.Property(x => x.Name)
                .HasMaxLength(120)
                .IsRequired();

            b.Property(x => x.TimeZone)
                .HasMaxLength(60)
                .IsRequired();

            b.Property(x => x.Is24x7)
                .IsRequired();

            b.Property(x => x.CreatedAt)
                .IsRequired();

            b.HasIndex(x => x.Name)
                .IsUnique();
        }
    }
}
