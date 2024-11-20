namespace Engage.Persistence.Configurations;

public class EmployeeLoanConfiguration : IEntityTypeConfiguration<EmployeeLoan>
{
    public void Configure(EntityTypeBuilder<EmployeeLoan> builder)
    {
        builder.Property(e => e.Amount)
            .IsRequired();

        builder.Property(e => e.RepayableAmount)
            .IsRequired();

        builder.Property(e => e.LoanTerm)
            .IsRequired();

        builder.Property(e => e.Installment)
            .IsRequired();

        builder.Property(e => e.Reason)
            .IsRequired();
        //.HasColumnType("ntext");

        builder.HasOne(x => x.Employee)
            .WithMany(s => s.Loans)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}
