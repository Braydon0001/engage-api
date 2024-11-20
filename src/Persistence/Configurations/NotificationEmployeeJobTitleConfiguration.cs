// auto-generated
namespace Engage.Persistence.Configurations;

public class NotificationEmployeeJobTitleConfiguration : IEntityTypeConfiguration<NotificationEmployeeJobTitle>
{
    public void Configure(EntityTypeBuilder<NotificationEmployeeJobTitle> builder)
    {
        builder.Property(e => e.EmployeeJobTitleId).IsRequired();

        // Multi-column indexes 

        builder.HasIndex(e => new { e.NotificationId, e.EmployeeJobTitleId }).IsUnique();
    }
}