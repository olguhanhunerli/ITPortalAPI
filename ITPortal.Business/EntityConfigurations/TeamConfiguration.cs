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
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> b)
        {
            b.HasKey(x => x.Id);

            b.HasOne(t => t.Department)
             .WithMany(d => d.Teams)
             .HasForeignKey(t => t.DepartmentId)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
