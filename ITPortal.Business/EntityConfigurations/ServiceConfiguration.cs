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
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> b)
        {
            b.HasKey(x => x.Id);

            b.Property(x => x.Name)
                .HasMaxLength(160)
                .IsRequired();

            b.Property(x => x.NameTr)
                .HasMaxLength(160);

            b.HasOne(s => s.OwnerTeam)
                .WithMany()
                .HasForeignKey(s => s.OwnerTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasIndex(x => x.OwnerTeamId);
            b.HasIndex(x => x.IsActive);
            b.HasIndex(x => x.Name);
        }
    }
}
