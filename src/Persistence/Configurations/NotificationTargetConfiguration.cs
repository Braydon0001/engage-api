// auto-generated
namespace Engage.Persistence.Configurations;

public class NotificationTargetConfiguration : IEntityTypeConfiguration<NotificationTarget>
{
    public void Configure(EntityTypeBuilder<NotificationTarget> builder)
    {
        builder.Property(e => e.NotificationTargetId).IsRequired();
        builder.Property(e => e.NotificationId).IsRequired();
    }
}