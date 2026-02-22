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
    public class TicketCategoryConfiguration : IEntityTypeConfiguration<TicketCategory>
    {
        public void Configure(EntityTypeBuilder<TicketCategory> b)
        {
            b.HasKey(x => x.Id);

            b.Property(x => x.Code).HasMaxLength(60).IsRequired();
            b.HasIndex(x => x.Code).IsUnique();

            b.Property(x => x.NameTr).HasMaxLength(160);
            b.Property(x => x.NameEn).HasMaxLength(160);

            b.HasOne(x => x.Parent)
             .WithMany(x => x.Children)
             .HasForeignKey(x => x.ParentId)
             .OnDelete(DeleteBehavior.Restrict);
            
            b.HasOne(x => x.ApprovalType)
             .WithMany()
             .HasForeignKey(x => x.ApprovalTypeId)
             .OnDelete(DeleteBehavior.SetNull);

            b.HasOne(x => x.DefaultTeam)
             .WithMany()
             .HasForeignKey(x => x.DefaultTeamId)
             .OnDelete(DeleteBehavior.SetNull);

            b.Property(x => x.CreatedAt).IsRequired();
            b.Property(x => x.UpdatedAt).IsRequired();
        }
    }
}
