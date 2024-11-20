// auto-generated
namespace Engage.Persistence.Configurations;

public class EmployeeStoreCalendarTypeConfiguration : IEntityTypeConfiguration<EmployeeStoreCalendarType>
{
    public void Configure(EntityTypeBuilder<EmployeeStoreCalendarType> builder)
    {
        builder.Property(e => e.EmployeeStoreCalendarTypeId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}