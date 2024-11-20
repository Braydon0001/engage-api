namespace Engage.Persistence.Configurations;

public class EmployeeExpenseClaimConfiguration : IEntityTypeConfiguration<EmployeeExpenseClaim>
{
    public void Configure(EntityTypeBuilder<EmployeeExpenseClaim> builder)
    {
        builder.Property(e => e.RecoverFrom)
            .HasMaxLength(120);

        builder.Property(e => e.Value)
            .IsRequired();

        builder.HasOne(x => x.Employee)
            .WithMany(e => e.ExpenseClaims)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}
