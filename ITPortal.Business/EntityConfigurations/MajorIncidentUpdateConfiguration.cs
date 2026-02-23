using ITPortal.Entities.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Business.EntityConfigurations
{
    public class MajorIncidentUpdateConfiguration : IEntityTypeConfiguration<MajorIncidentUpdate>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<MajorIncidentUpdate> b)
        {
            b.HasKey(x => x.Id);

            b.Property(x => x.Message)
                .IsRequired()
                .HasMaxLength(4000); 

            b.Property(x => x.CreatedAt).IsRequired();

            b.HasOne(x => x.MajorIncident)
                .WithMany(mi => mi.Updates)
                .HasForeignKey(x => x.MajorIncidentId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.Author)
                .WithMany()
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasIndex(x => new { x.MajorIncidentId, x.CreatedAt });
        }
    }
}
