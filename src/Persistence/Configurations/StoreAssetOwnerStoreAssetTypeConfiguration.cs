namespace Engage.Persistence.Configurations;

public class StoreAssetOwnerStoreAssetTypeConfiguration : IEntityTypeConfiguration<StoreAssetOwnerStoreAssetType>
{
    public void Configure(EntityTypeBuilder<StoreAssetOwnerStoreAssetType> builder)
    {
        builder.Property(e => e.StoreAssetOwnerStoreAssetTypeId).IsRequired();
        builder.Property(e => e.StoreAssetTypeId).IsRequired();
        builder.Property(e => e.StoreAssetOwnerId).IsRequired();
    }
}
