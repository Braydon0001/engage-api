namespace Engage.Persistence.Configurations;

public class StoreConceptAttributeStoreAssetConfiguration : IEntityTypeConfiguration<StoreConceptAttributeStoreAsset>
{
    public void Configure(EntityTypeBuilder<StoreConceptAttributeStoreAsset> builder)
    {
        builder.HasKey(e => new { e.StoreConceptAttributeId, e.StoreAssetId }).IsClustered(false);

        builder.HasOne(x => x.StoreConceptAttribute)
            .WithMany(s => s.StoreConceptAttributeStoreAssets)
            .HasForeignKey(x => x.StoreConceptAttributeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.StoreAsset)
            .WithMany(s => s.StoreConceptAttributeStoreAssets)
            .HasForeignKey(x => x.StoreAssetId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
