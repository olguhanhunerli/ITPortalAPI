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
    public class TicketEventConfiguration : IEntityTypeConfiguration<TicketEvent>
    {
        public void Configure(EntityTypeBuilder<TicketEvent> b)
        {

            b.HasKey(x => x.Id);

            b.Property(x => x.EventType)
             .HasMaxLength(80)
             .IsRequired();

            b.Property(x => x.PayloadJson);

            b.HasIndex(x => x.TicketId);

            b.HasIndex(x => x.CreatedAt);

            b.HasOne(x => x.Ticket)
             .WithMany(t => t.Events)
             .HasForeignKey(x => x.TicketId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.Actor)
             .WithMany()
             .HasForeignKey(x => x.ActorId)
             .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
