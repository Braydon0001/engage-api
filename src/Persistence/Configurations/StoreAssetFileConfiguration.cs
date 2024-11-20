namespace Engage.Persistence.Configurations;

public class StoreAssetFileConfiguration : IEntityTypeConfiguration<StoreAssetFile>
{
    public void Configure(EntityTypeBuilder<StoreAssetFile> builder)
    {
        builder.Property(e => e.StoreAssetFileId).IsRequired();
        builder.Property(e => e.StoreAssetId).IsRequired();
        builder.Property(e => e.ImageUrl);
        builder.Property(e => e.StoreAssetFileTypeId).IsRequired();
        builder.Property(e => e.Files).HasColumnType("json");
    }
}