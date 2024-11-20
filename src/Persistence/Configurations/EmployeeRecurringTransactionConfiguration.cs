// auto-generated
namespace Engage.Persistence.Configurations;

public class EmployeeRecurringTransactionConfiguration : IEntityTypeConfiguration<EmployeeRecurringTransaction>
{
    public void Configure(EntityTypeBuilder<EmployeeRecurringTransaction> builder)
    {
        builder.Property(e => e.EmployeeRecurringTransactionId).IsRequired();
        builder.Property(e => e.EmployeeId).IsRequired();
        builder.Property(e => e.EmployeeTransactionTypeId).IsRequired();
        builder.Property(e => e.EmployeeRecurringTransactionStatusId).IsRequired();
        builder.Property(e => e.PayrollPeriodId).IsRequired();
        builder.Property(e => e.CreditorBankAccountId);
        builder.Property(e => e.StartDate);
        builder.Property(e => e.EndDate);
        builder.Property(e => e.InitialAmount);
        builder.Property(e => e.InstallmentAmount);
        builder.Property(e => e.BaseInstallmentOnAmountOrComponent).HasMaxLength(100);
        builder.Property(e => e.Note).HasMaxLength(220);
        builder.Property(e => e.Reference).HasMaxLength(220);
        builder.Property(e => e.IsFringeBenefitLoan);
        builder.Property(e => e.LeavePayPercentage);
    }
}