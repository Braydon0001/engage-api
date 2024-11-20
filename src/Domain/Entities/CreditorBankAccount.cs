// auto-generated
namespace Engage.Domain.Entities;

public class CreditorBankAccount : BaseAuditableEntity
{
    public int CreditorBankAccountId { get; set; }
    public int BankNameId { get; set; }
    public int BankAccountTypeId { get; set; }
    public string Name { get; set; }
    public string AccountNumber { get; set; }
    public string BranchCode { get; set; }

    // Navigation Properties

    public BankName BankName { get; set; }
    public BankAccountType BankAccountType { get; set; }
}