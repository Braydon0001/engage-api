// auto-generated
namespace Engage.Persistence.Configurations;

public class NotificationEmployeeConfiguration : IEntityTypeConfiguration<NotificationEmployee>
{
    public void Configure(EntityTypeBuilder<NotificationEmployee> builder)
    {
        builder.Property(e => e.EmployeeId).IsRequired();

        // Multi-column indexes 

        builder.HasIndex(e => new { e.NotificationId, e.EmployeeId }).IsUnique();
    }
}