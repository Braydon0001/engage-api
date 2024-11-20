namespace Engage.Persistence.Configurations;

public class EmployeeNotificationConfiguration : IEntityTypeConfiguration<EmployeeNotification>
{
    public void Configure(EntityTypeBuilder<EmployeeNotification> builder)
    {
        builder.HasKey(e => new { e.EmployeeId, e.NotificationId })
            .IsClustered(false);

        builder.HasOne(x => x.Employee)
            .WithMany(s => s.EmployeeNotifications)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.Notification)
            .WithMany(s => s.EmployeeNotifications)
            .HasForeignKey(x => x.NotificationId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}
