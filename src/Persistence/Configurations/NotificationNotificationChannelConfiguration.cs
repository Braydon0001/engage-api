namespace Engage.Persistence.Configurations;

public class NotificationNotificationChannelConfiguration : IEntityTypeConfiguration<NotificationNotificationChannel>
{
    public void Configure(EntityTypeBuilder<NotificationNotificationChannel> builder)
    {
        builder.HasKey(e => new { e.NotificationId, e.NotificationChannelId })
            .IsClustered(false);

        builder.HasOne(x => x.Notification)
            .WithMany(c => c.NotificationChannels)
            .HasForeignKey(x => x.NotificationId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.NotificationChannel)
            .WithMany(c => c.NotificationChannels)
            .HasForeignKey(x => x.NotificationChannelId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
