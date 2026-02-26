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
    public class HolidayConfiguration : IEntityTypeConfiguration<Holiday>
    {
        public void Configure(EntityTypeBuilder<Holiday> b)
        {

            b.HasKey(x => x.Id);

            b.Property(x => x.NameTr)
                .HasMaxLength(160)
                .IsRequired();

            b.Property(x => x.NameEn)
                .HasMaxLength(160)
                .IsRequired();

            b.Property(x => x.IsFullDay)
                .IsRequired();

            b.Property(x => x.CreatedAt)
                .IsRequired();

            b.HasIndex(x => new { x.HolidayDate, x.LocationId })
                .IsUnique();

            b.HasIndex(x => x.LocationId)

            b.HasOne(x => x.Location)
                .WithMany(l => l.Holidays)
                .HasForeignKey(x => x.LocationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
