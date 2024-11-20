// auto-generated
namespace Engage.Persistence.Configurations;

public class NotificationStoreConfiguration : IEntityTypeConfiguration<NotificationStore>
{
    public void Configure(EntityTypeBuilder<NotificationStore> builder)
    {
        builder.Property(e => e.StoreId).IsRequired();

        // Multi-column indexes 

        builder.HasIndex(e => new { e.NotificationId, e.StoreId }).IsUnique();
    }
}