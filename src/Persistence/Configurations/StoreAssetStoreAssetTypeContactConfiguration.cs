namespace Engage.Persistence.Configurations;

public class StoreAssetStoreAssetTypeContactConfiguration : IEntityTypeConfiguration<StoreAssetStoreAssetTypeContact>
{
    public void Configure(EntityTypeBuilder<StoreAssetStoreAssetTypeContact> builder)
    {
        builder.Property(e => e.StoreAssetStoreAssetTypeContactId).IsRequired();
        builder.Property(e => e.StoreAssetId).IsRequired();
        builder.Property(e => e.StoreAssetTypeContactId).IsRequired();
    }
}