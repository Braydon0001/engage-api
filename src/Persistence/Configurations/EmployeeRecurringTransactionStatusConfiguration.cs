// auto-generated
namespace Engage.Persistence.Configurations;

public class EmployeeRecurringTransactionStatusConfiguration : IEntityTypeConfiguration<EmployeeRecurringTransactionStatus>
{
    public void Configure(EntityTypeBuilder<EmployeeRecurringTransactionStatus> builder)
    {
        builder.Property(e => e.EmployeeRecurringTransactionStatusId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}