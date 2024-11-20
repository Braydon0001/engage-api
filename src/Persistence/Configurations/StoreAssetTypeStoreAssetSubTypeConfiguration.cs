namespace Engage.Persistence.Configurations;

public class StoreAssetTypeStoreAssetSubTypeConfiguration : IEntityTypeConfiguration<StoreAssetTypeStoreAssetSubType>
{
    public void Configure(EntityTypeBuilder<StoreAssetTypeStoreAssetSubType> builder)
    {
        builder.Property(e => e.StoreAssetTypeStoreAssetSubTypeId).IsRequired();
        builder.Property(e => e.StoreAssetTypeId).IsRequired();
        builder.Property(e => e.StoreAssetSubTypeId).IsRequired();
    }
}
