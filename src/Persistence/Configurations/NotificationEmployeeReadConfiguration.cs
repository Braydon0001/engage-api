namespace Engage.Persistence.Configurations
{
    public class NotificationEmployeeReadConfiguration : IEntityTypeConfiguration<NotificationEmployeeRead>
    {
        public void Configure(EntityTypeBuilder<NotificationEmployeeRead> builder)
        {
            builder.HasKey(e => new { e.NotificationId, e.EmployeeId }).IsClustered(false);

            builder.HasOne(x => x.Notification)
                .WithMany(s => s.NotificationEmployeeReads)
                .HasForeignKey(x => x.NotificationId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.Employee)
                .WithMany(s => s.NotificationEmployeeReads)
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }

}
