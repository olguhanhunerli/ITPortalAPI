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
    public class MajorIncidentConfiguration : IEntityTypeConfiguration<MajorIncident>
    {
        public void Configure(EntityTypeBuilder<MajorIncident> b)
        {
            b.HasKey(x => x.Id);

            b.HasOne(mi => mi.Ticket)
                .WithOne(t => t.MajorIncident)
                .HasForeignKey<MajorIncident>(mi => mi.TicketId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasIndex(mi => mi.TicketId).IsUnique();

            b.HasOne(mi => mi.Status)
                .WithMany()
                .HasForeignKey(mi => mi.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(mi => mi.Lead)
                .WithMany()
                .HasForeignKey(mi => mi.LeadId)
                .OnDelete(DeleteBehavior.Restrict);

            b.Property(mi => mi.DeclaredAt).IsRequired();

            b.HasMany(mi => mi.Updates)
                .WithOne(u => u.MajorIncident)
                .HasForeignKey(u => u.MajorIncidentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
