// auto-generated
namespace Engage.Persistence.Configurations;

public class WebFileStoreFormatConfiguration : IEntityTypeConfiguration<WebFileStoreFormat>
{
    public void Configure(EntityTypeBuilder<WebFileStoreFormat> builder)
    {
        builder.Property(e => e.StoreFormatId).IsRequired();

        // Multi-column indexes 

        builder.HasIndex(e => new { e.WebFileId, e.StoreFormatId }).IsUnique();
    }
}