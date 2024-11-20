namespace Engage.Domain.Entities;

public class EmployeeBankDetail : BaseAuditableEntity
{
    public int EmployeeBankDetailId { get; set; }
    public int EmployeeId { get; set; }
    public int BankAccountOwnerId { get; set; }
    public int BankAccountTypeId { get; set; }
    public int BankPaymentMethodId { get; set; }
    public int BankNameId { get; set; }
    public string BranchCode { get; set; }
    public string AccountNumber { get; set; }
    public string AccountHolder { get; set; }
    public string BeneficiaryReference { get; set; }
    public string SwiftCode { get; set; }
    public string RoutingCode { get; set; }
    public bool IsPrimary { get; set; }
    public string Note { get; set; }
    public List<JsonFile> Files { get; set; }

    // Navigation Properties
    public Employee Employee { get; set; }
    public BankPaymentMethod BankPaymentMethod { get; set; }
    public BankAccountOwner BankAccountOwner { get; set; }
    public BankAccountType BankAccountType { get; set; }
    public BankName BankName { get; set; }
}
