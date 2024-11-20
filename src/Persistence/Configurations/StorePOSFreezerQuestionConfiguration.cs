using Engage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations
{
    class StorePOSFreezerConfiguration : IEntityTypeConfiguration<StorePOSFreezerQuestion>
    {
        public void Configure(EntityTypeBuilder<StorePOSFreezerQuestion> builder)
        {
            builder.HasIndex(e => new { e.StoreId, e.StorePOSTypeId })
                    .IsUnique();

            builder.Property(e => e.WobblersComment)
                  .IsRequired()
                  .HasMaxLength(1000);

            builder.Property(e => e.FreezerDecalsComment)
                   .IsRequired()
                   .HasMaxLength(1000);
            
            builder.Property(e => e.ShelfTalkerComment)
                   .IsRequired()
                   .HasMaxLength(1000);
        }
    }
}
