// auto-generated
namespace Engage.Persistence.Configurations;

public class EmployeeStoreCalendarPeriodConfiguration : IEntityTypeConfiguration<EmployeeStoreCalendarPeriod>
{
    public void Configure(EntityTypeBuilder<EmployeeStoreCalendarPeriod> builder)
    {
        builder.Property(e => e.EmployeeStoreCalendarPeriodId).IsRequired();
        builder.Property(e => e.EmployeeStoreCalendarYearId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Number).IsRequired();
        builder.Property(e => e.StartDate).IsRequired();
        builder.Property(e => e.EndDate).IsRequired();

        // Multi-column indexes 

        builder.HasIndex(e => new { e.EmployeeStoreCalendarYearId, e.Number }).IsUnique();
    }
}