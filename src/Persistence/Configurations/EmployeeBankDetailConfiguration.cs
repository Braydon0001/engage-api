namespace Engage.Persistence.Configurations;

public class EmployeeBankDetailConfiguration : IEntityTypeConfiguration<EmployeeBankDetail>
{
    public void Configure(EntityTypeBuilder<EmployeeBankDetail> builder)
    {
        builder.Property(e => e.BranchCode).IsRequired().HasMaxLength(30);
        builder.Property(e => e.AccountNumber).IsRequired().HasMaxLength(30);
        builder.Property(e => e.AccountHolder).IsRequired().HasMaxLength(30);
        builder.Property(e => e.BeneficiaryReference).HasMaxLength(100);
        builder.Property(e => e.SwiftCode).HasMaxLength(100);
        builder.Property(e => e.RoutingCode).HasMaxLength(100);
        builder.Property(e => e.Files).HasColumnType("json");

        builder.HasOne(x => x.Employee)
            .WithMany(s => s.BankDetails)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}
