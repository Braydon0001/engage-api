// auto-generated
namespace Engage.Persistence.Configurations;

public class EmployeeStoreCalendarYearConfiguration : IEntityTypeConfiguration<EmployeeStoreCalendarYear>
{
    public void Configure(EntityTypeBuilder<EmployeeStoreCalendarYear> builder)
    {
        builder.Property(e => e.EmployeeStoreCalendarYearId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.StartDate);
        builder.Property(e => e.EndDate);
    }
}