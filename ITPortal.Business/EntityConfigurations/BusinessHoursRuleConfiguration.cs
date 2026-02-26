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
    public class BusinessHoursRuleConfiguration : IEntityTypeConfiguration<BusinessHoursRule>
    {
        public void Configure(EntityTypeBuilder<BusinessHoursRule> b)
        {

            b.HasKey(x => x.Id);

            b.Property(x => x.DayOfWeek)
                .IsRequired();

            b.Property(x => x.StartTime)
                .IsRequired();

            b.Property(x => x.EndTime)
                .IsRequired();


            b.HasOne(x => x.BusinessHours)
                .WithMany(bh => bh.Rules)
                .HasForeignKey(x => x.BusinessHoursId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
