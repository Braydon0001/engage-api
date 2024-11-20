// auto-generated
namespace Engage.Persistence.Configurations;

public class EmployeeStoreCalendarGroupConfiguration : IEntityTypeConfiguration<EmployeeStoreCalendarGroup>
{
    public void Configure(EntityTypeBuilder<EmployeeStoreCalendarGroup> builder)
    {
        builder.Property(e => e.EmployeeStoreCalendarGroupId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Number).IsRequired();
    }
}