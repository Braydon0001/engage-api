// auto-generated
namespace Engage.Persistence.Configurations;

public class WebFileStoreConfiguration : IEntityTypeConfiguration<WebFileStore>
{
    public void Configure(EntityTypeBuilder<WebFileStore> builder)
    {
        builder.Property(e => e.StoreId).IsRequired();

        // Multi-column indexes 

        builder.HasIndex(e => new { e.WebFileId, e.StoreId }).IsUnique();
    }
}