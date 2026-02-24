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
    public class ConfigurationItemConfiguration : IEntityTypeConfiguration<ConfigurationItem>
    {
        public void Configure(EntityTypeBuilder<ConfigurationItem> b)
        {

            b.HasKey(x => x.Id);

            b.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            b.Property(x => x.SerialNumber)
                .HasMaxLength(120);

            b.Property(x => x.AssetTag)
                .HasMaxLength(120);


            b.HasOne(x => x.CiType)
                .WithMany()
                .HasForeignKey(x => x.CiTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(x => x.CiStatus)
                .WithMany()
                .HasForeignKey(x => x.CiStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(x => x.OwnerUser)
                .WithMany()
                .HasForeignKey(x => x.OwnerUserId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(x => x.OwnerTeam)
                .WithMany()
                .HasForeignKey(x => x.OwnerTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(x => x.Location)
                .WithMany()
                .HasForeignKey(x => x.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasIndex(x => x.CiTypeId);
            b.HasIndex(x => x.CiStatusId);
            b.HasIndex(x => x.OwnerUserId);
            b.HasIndex(x => x.OwnerTeamId);
            b.HasIndex(x => x.LocationId);

            b.HasQueryFilter(x => x.DeletedAt == null);
        }
    }
}
