// auto-generated
namespace Engage.Persistence.Configurations;

public class EmployeeTransactionConfiguration : IEntityTypeConfiguration<EmployeeTransaction>
{
    public void Configure(EntityTypeBuilder<EmployeeTransaction> builder)
    {
        builder.Property(e => e.EmployeeTransactionId).IsRequired();
        builder.Property(e => e.EmployeeId).IsRequired();
        builder.Property(e => e.EmployeeTransactionTypeId).IsRequired();
        builder.Property(e => e.EmployeeRecurringTransactionId);
        builder.Property(e => e.EmployeeTransactionStatusId);
        builder.Property(e => e.EmployeeRecurringTransactionStatusId);
        builder.Property(e => e.PayrollPeriodId).IsRequired();
        builder.Property(e => e.EmployeeRecurringTransactionCount);
        builder.Property(e => e.TransactionDate);
        builder.Property(e => e.Amount);
        builder.Property(e => e.Rate);
        builder.Property(e => e.Days);
        builder.Property(e => e.Hours);
        builder.Property(e => e.UnpaidDays);
        builder.Property(e => e.UnpaidHours);
        builder.Property(e => e.Note).HasMaxLength(220);
        builder.Property(e => e.EmployeeTransactionRemunerationTypeId);
        builder.Property(e => e.StartDate);
        builder.Property(e => e.EndDate);
    }
}