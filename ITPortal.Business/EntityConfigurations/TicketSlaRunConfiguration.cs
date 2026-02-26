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
    public class TicketSlaRunConfiguration : IEntityTypeConfiguration<TicketSlaRun>
    {
        public void Configure(EntityTypeBuilder<TicketSlaRun> b)
        {

            b.HasKey(x => x.Id);

            b.Property(x => x.CreatedAt)
                .IsRequired();

            b.HasIndex(x => x.TicketId)
                .IsUnique();
            b.HasIndex(x => x.SlaPolicyId);

            b.HasOne(x => x.Ticket)
                .WithOne() 
                .HasForeignKey<TicketSlaRun>(x => x.TicketId)
                .OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.Ticket)
                .WithOne(t => t.SlaRun)
                .HasForeignKey<TicketSlaRun>(x => x.TicketId)
                .OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.SlaPolicy)
                .WithMany() 
                .HasForeignKey(x => x.SlaPolicyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
