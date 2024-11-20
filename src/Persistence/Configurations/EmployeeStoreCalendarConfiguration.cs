// auto-generated
namespace Engage.Persistence.Configurations;

public class EmployeeStoreCalendarConfiguration : IEntityTypeConfiguration<EmployeeStoreCalendar>
{
    public void Configure(EntityTypeBuilder<EmployeeStoreCalendar> builder)
    {
        builder.Property(e => e.EmployeeStoreCalendarId).IsRequired();
        builder.Property(e => e.CompletionDate);
        builder.Property(e => e.EmployeeId).IsRequired();
        builder.Property(e => e.StoreId).IsRequired();
        builder.Property(e => e.EmployeeStoreCalendarTypeId);
        builder.Property(e => e.EmployeeStoreCalendarStatusId);
        builder.Property(e => e.IsManagerCreated);
        builder.Property(e => e.CalendarDate).IsRequired();
        builder.Property(e => e.Order);
        builder.Property(e => e.EmployeeStoreCalendarPeriodId).IsRequired();
        builder.Property(e => e.EmployeeStoreCalendarGroupId).IsRequired();
        builder.Property(e => e.SurveyInstanceId);
        builder.Property(e => e.Note).HasMaxLength(200);
        builder.Property(e => e.EmailedTo).HasMaxLength(1000);

        // Multi-column indexes 

        builder.HasIndex(e => new { e.EmployeeId, e.StoreId, e.CalendarDate }).IsUnique();
    }
}