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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> b)
        {
            b.HasKey(x => x.Id);

            b.HasOne(u => u.Department)
             .WithMany(d => d.Users)
             .HasForeignKey(u => u.DepartmentId)
             .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(u => u.Team)
             .WithMany(t => t.Users)
             .HasForeignKey(u => u.TeamId)
             .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(u => u.Location)
             .WithMany(l => l.Users)
             .HasForeignKey(u => u.LocationId)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
