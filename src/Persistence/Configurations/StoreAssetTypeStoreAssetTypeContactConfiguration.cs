namespace Engage.Persistence.Configurations;

public class StoreAssetTypeStoreAssetTypeContactConfiguration : IEntityTypeConfiguration<StoreAssetTypeStoreAssetTypeContact>
{
    public void Configure(EntityTypeBuilder<StoreAssetTypeStoreAssetTypeContact> builder)
    {
        builder.Property(e => e.StoreAssetTypeStoreAssetTypeContactId).IsRequired();
        builder.Property(e => e.StoreAssetTypeId).IsRequired();
        builder.Property(e => e.StoreAssetTypeContactId).IsRequired();
    }
}