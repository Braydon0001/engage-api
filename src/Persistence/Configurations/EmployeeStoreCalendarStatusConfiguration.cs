// auto-generated
namespace Engage.Persistence.Configurations;

public class EmployeeStoreCalendarStatusConfiguration : IEntityTypeConfiguration<EmployeeStoreCalendarStatus>
{
    public void Configure(EntityTypeBuilder<EmployeeStoreCalendarStatus> builder)
    {
        builder.Property(e => e.EmployeeStoreCalendarStatusId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}