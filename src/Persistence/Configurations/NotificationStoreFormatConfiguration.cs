// auto-generated
namespace Engage.Persistence.Configurations;

public class NotificationStoreFormatConfiguration : IEntityTypeConfiguration<NotificationStoreFormat>
{
    public void Configure(EntityTypeBuilder<NotificationStoreFormat> builder)
    {
        builder.Property(e => e.StoreFormatId).IsRequired();

        // Multi-column indexes 

        builder.HasIndex(e => new { e.NotificationId, e.StoreFormatId }).IsUnique();
    }
}