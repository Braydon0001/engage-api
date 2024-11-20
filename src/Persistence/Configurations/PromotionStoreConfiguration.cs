using Engage.Domain.Entities.LinkEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations
{
    public class PromotionStoreConfiguration : IEntityTypeConfiguration<PromotionStore>
    {
        public void Configure(EntityTypeBuilder<PromotionStore> builder)
        {
            builder.HasKey(e => new { e.PromotionId, e.StoreId })
                .IsClustered(false);

            builder.HasOne(x => x.Promotion)
                .WithMany(s => s.PromotionStores)
                .HasForeignKey(x => x.PromotionId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.Store)
                .WithMany(s => s.PromotionStores)
                .HasForeignKey(x => x.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
