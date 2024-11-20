// auto-generated
namespace Engage.Persistence.Configurations;

public class EmployeeStoreCalendarBlockDayConfiguration : IEntityTypeConfiguration<EmployeeStoreCalendarBlockDay>
{
    public void Configure(EntityTypeBuilder<EmployeeStoreCalendarBlockDay> builder)
    {
        builder.Property(e => e.EmployeeStoreCalendarBlockDayId).IsRequired();
        builder.Property(e => e.EmployeeId).IsRequired();
        builder.Property(e => e.EmployeeStoreCalendarTypeId).IsRequired();
        builder.Property(e => e.EmployeeStoreCalendarStatusId).IsRequired();
        builder.Property(e => e.CalendarDate).IsRequired();
        builder.Property(e => e.IsManagerCreated);
        builder.Property(e => e.EmployeeStoreCalendarPeriodId).IsRequired();
        builder.Property(e => e.Note).IsRequired().HasMaxLength(100);
    }
}