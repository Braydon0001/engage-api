using Engage.Domain.Entities.LinkEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations
{
    public class EngageProductTagConfiguration : IEntityTypeConfiguration<EngageProductTag>
    {
        public void Configure(EntityTypeBuilder<EngageProductTag> builder)
        {
            builder.HasKey(e => new { e.EngageMasterProductId, e.EngageTagId })
                .IsClustered(false);

            builder.HasOne(x => x.EngageMasterProduct)
                .WithMany(e => e.EngageProductTags)
                .HasForeignKey(x => x.EngageMasterProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.EngageTag)
                .WithMany(e => e.EngageProductTags)
                .HasForeignKey(x => x.EngageTagId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
