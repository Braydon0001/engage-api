using Engage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.Property(e => e.Province)
                .IsRequired()
                .HasMaxLength(120);

            builder.Property(e => e.City)
                .IsRequired()
                .HasMaxLength(120);

            builder.Property(e => e.BusinessUnit)
                .HasMaxLength(120);

            builder.Property(e => e.AddressLineOne)
                .IsRequired()
                .HasMaxLength(220);

            builder.Property(e => e.AddressLineTwo)
                .IsRequired()
                .HasMaxLength(220);

            builder.Property(e => e.Suburb)
              .HasMaxLength(120);

            builder.Property(e => e.PostCode)
              .HasMaxLength(30);

            //builder.Property(e => e.Note)
            //    .HasColumnType("ntext");

            builder.HasOne(e => e.Stakeholder)
                .WithMany(s => s.Locations)
                .HasForeignKey(e => e.StakeholderId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
