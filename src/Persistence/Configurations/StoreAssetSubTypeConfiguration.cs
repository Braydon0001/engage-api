// auto-generated
namespace Engage.Persistence.Configurations;

public class StoreAssetSubTypeConfiguration : IEntityTypeConfiguration<StoreAssetSubType>
{
    public void Configure(EntityTypeBuilder<StoreAssetSubType> builder)
    {
        builder.Property(e => e.StoreAssetSubTypeId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.StoreAssetTypeId);
    }
}