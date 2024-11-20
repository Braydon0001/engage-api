using Engage.Domain.Entities.LinkEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations
{
    public class StoreStoreListConfiguration : IEntityTypeConfiguration<StoreStoreList>
    {
        public void Configure(EntityTypeBuilder<StoreStoreList> builder)
        {
            builder.HasKey(e => new { e.StoreId, e.StoreListId })
                .IsClustered(false);

            builder.HasOne(x => x.Store)
                .WithMany(e => e.StoreStoreLists)
                .HasForeignKey(x => x.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.StoreList)
                .WithMany(e => e.StoreStoreLists)
                .HasForeignKey(x => x.StoreListId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
