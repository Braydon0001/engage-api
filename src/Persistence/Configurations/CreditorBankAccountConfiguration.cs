// auto-generated
namespace Engage.Persistence.Configurations;

public class CreditorBankAccountConfiguration : IEntityTypeConfiguration<CreditorBankAccount>
{
    public void Configure(EntityTypeBuilder<CreditorBankAccount> builder)
    {
        builder.Property(e => e.CreditorBankAccountId).IsRequired();
        builder.Property(e => e.BankNameId).IsRequired();
        builder.Property(e => e.BankAccountTypeId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(120);
        builder.Property(e => e.AccountNumber).IsRequired().HasMaxLength(100);
        builder.Property(e => e.BranchCode).IsRequired().HasMaxLength(15);
    }
}