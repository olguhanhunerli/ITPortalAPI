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
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> b)
        {
            b.HasKey(x => x.Id);

            b.Property(x => x.TicketNumber)
                .HasMaxLength(30)
                .IsRequired();

            b.HasIndex(x => x.TicketNumber)
                .IsUnique();

            b.Property(x => x.Title)
                .HasMaxLength(200)
                .IsRequired();

            b.HasOne(x => x.Type)
                .WithMany()
                .HasForeignKey(x => x.TypeId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(x => x.Status)
                .WithMany()
                .HasForeignKey(x => x.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(x => x.Priority)
                .WithMany()
                .HasForeignKey(x => x.PriorityId)
                .OnDelete(DeleteBehavior.SetNull);

            b.HasOne(x => x.Impact)
                .WithMany()
                .HasForeignKey(x => x.ImpactId)
                .OnDelete(DeleteBehavior.SetNull);

            b.HasOne(x => x.Urgency)
                .WithMany()
                .HasForeignKey(x => x.UrgencyId)
                .OnDelete(DeleteBehavior.SetNull);

            b.HasOne(x => x.ApprovalState)
                .WithMany()
                .HasForeignKey(x => x.ApprovalStateId)
                .OnDelete(DeleteBehavior.SetNull);

            b.HasOne(x => x.Category)
               .WithMany(c => c.CategoryTickets)  
               .HasForeignKey(x => x.CategoryId)
               .OnDelete(DeleteBehavior.SetNull);

            b.HasOne(x => x.Subcategory)
              .WithMany(c => c.SubcategoryTickets) 
              .HasForeignKey(x => x.SubcategoryId)
              .OnDelete(DeleteBehavior.SetNull);

            b.HasOne(x => x.Requester)
                .WithMany()
                .HasForeignKey(x => x.RequesterId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(x => x.RequestedFor)
                .WithMany()
                .HasForeignKey(x => x.RequestedForId)
                .OnDelete(DeleteBehavior.SetNull);

            b.HasOne(x => x.Assignee)
                .WithMany()
                .HasForeignKey(x => x.AssigneeId)
                .OnDelete(DeleteBehavior.SetNull);

            b.HasOne(x => x.AssignedTeam)
                .WithMany()
                .HasForeignKey(x => x.AssignedTeamId)
                .OnDelete(DeleteBehavior.SetNull);

            b.HasOne(x => x.Location)
                .WithMany()
                .HasForeignKey(x => x.LocationId)
                .OnDelete(DeleteBehavior.SetNull);
           
            b.HasOne(x => x.Department)
                .WithMany()
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);

            b.HasMany(x => x.Comments)
                .WithOne(c => c.Ticket)
                .HasForeignKey(c => c.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(x => x.Events)
                .WithOne(e => e.Ticket)
                .HasForeignKey(e => e.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            b.Property(x => x.CreatedAt).IsRequired();
            b.Property(x => x.UpdatedAt).IsRequired();
        }
    }
}
