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
    public class TicketCommentConfiguration : IEntityTypeConfiguration<TicketComment>
    {
        public void Configure(EntityTypeBuilder<TicketComment> b)
        {
            b.HasKey(x => x.Id);

            b.Property(x => x.Body)
             .IsRequired();

            b.HasIndex(x => x.TicketId);

            b.HasIndex(x => x.CreatedAt);

            b.HasOne(x => x.Ticket)
             .WithMany(t => t.Comments)
             .HasForeignKey(x => x.TicketId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.Author)
             .WithMany()
             .HasForeignKey(x => x.AuthorId)
             .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(x => x.Visibility)
             .WithMany()
             .HasForeignKey(x => x.VisibilityId)
             .OnDelete(DeleteBehavior.Restrict);

            b.Property(x => x.CreatedAt).IsRequired();
        }
    }
}
