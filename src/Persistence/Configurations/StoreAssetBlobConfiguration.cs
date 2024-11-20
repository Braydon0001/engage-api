namespace Engage.Persistence.Configurations;

public class StoreAssetBlobConfiguration : IEntityTypeConfiguration<StoreAssetBlob>
{
    public void Configure(EntityTypeBuilder<StoreAssetBlob> builder)
    {
        builder.Property(e => e.Files).HasColumnType("json");
        builder.Property(e => e.StoreAssetFileTypeId);
    }
}
