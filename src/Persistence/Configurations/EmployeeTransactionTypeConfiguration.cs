// auto-generated
namespace Engage.Persistence.Configurations;

public class EmployeeTransactionTypeConfiguration : IEntityTypeConfiguration<EmployeeTransactionType>
{
    public void Configure(EntityTypeBuilder<EmployeeTransactionType> builder)
    {
        builder.Property(e => e.EmployeeTransactionTypeId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.IsPositive);
        builder.Property(e => e.IsRecurring);
        builder.Property(e => e.Fields).HasColumnType("json");
        builder.Property(e => e.OvertimeMultiple);
    }
}