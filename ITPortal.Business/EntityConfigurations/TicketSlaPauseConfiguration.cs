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
    public class TicketSlaPauseConfiguration : IEntityTypeConfiguration<TicketSlaPause>
    {
        public void Configure(EntityTypeBuilder<TicketSlaPause> b)
        {
            b.HasKey(x => x.Id);

            b.Property(x => x.Note)
                .HasMaxLength(800);

            b.Property(x => x.StartedAt)
                .IsRequired();

            b.HasOne(x => x.Ticket)
                .WithMany(t => t.SlaPauses)
                .HasForeignKey(x => x.TicketId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(x => x.Reason)
                .WithMany()
                .HasForeignKey(x => x.ReasonId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(x => x.CreatedBy)
                .WithMany(u => u.CreatedTicketSlaPauses)
                .HasForeignKey(x => x.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
