// auto-generated
namespace Engage.Persistence.Configurations;

public class NotificationEngageRegionConfiguration : IEntityTypeConfiguration<NotificationEngageRegion>
{
    public void Configure(EntityTypeBuilder<NotificationEngageRegion> builder)
    {
        builder.Property(e => e.EngageRegionId).IsRequired();

        // Multi-column indexes 

        builder.HasIndex(e => new { e.NotificationId, e.EngageRegionId }).IsUnique();
    }
}