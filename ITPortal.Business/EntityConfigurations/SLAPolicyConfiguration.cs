using ITPortal.Entities.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Business.EntityConfigurations
{
    public class SLAPolicyConfiguration : IEntityTypeConfiguration<SlaPolicy>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SlaPolicy> b)
        {

            b.HasKey(x => x.Id);

            b.Property(x => x.Name)
                .HasMaxLength(120)
                .IsRequired();

            b.Property(x => x.FirstResponseMinutes)
                .IsRequired();

            b.Property(x => x.ResolutionMinutes)
                .IsRequired();

            b.Property(x => x.IsActive)
                .IsRequired();

            b.Property(x => x.CreatedAt)
                .IsRequired();

            b.Property(x => x.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .IsRequired();

            b.HasOne(x => x.Priority)
                .WithMany()
                .HasForeignKey(x => x.PriorityId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(x => x.AppliesToType)
                .WithMany()
                .HasForeignKey(x => x.AppliesToTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(x => x.BusinessHours)
                .WithMany(x => x.SlaPolicies)
                .HasForeignKey(x => x.BusinessHoursId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasIndex(x => x.PriorityId);
            b.HasIndex(x => x.AppliesToTypeId);
            b.HasIndex(x => x.BusinessHoursId);

            b.HasIndex(x => new { x.AppliesToTypeId, x.PriorityId })
                .IsUnique();

            b.HasQueryFilter(x => x.DeletedAt == null);
        }
    }
}
