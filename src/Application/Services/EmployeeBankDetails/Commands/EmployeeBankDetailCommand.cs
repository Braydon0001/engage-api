namespace Engage.Application.Services.EmployeeBankDetails.Commands;

public class EmployeeBankDetailCommand : IMapTo<EmployeeBankDetail>
{
    public int BankPaymentMethodId { get; set; }
    public int BankAccountOwnerId { get; set; }
    public int BankAccountTypeId { get; set; }
    public int BankNameId { get; set; }
    public string BranchCode { get; set; }
    public string AccountNumber { get; set; }
    public string AccountHolder { get; set; }
    public string BeneficiaryReference { get; set; }
    public string SwiftCode { get; set; }
    public string RoutingCode { get; set; }
    public bool IsPrimary { get; set; }
    public string Note { get; set; }
    public bool IsDisabled { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeBankDetailCommand, EmployeeBankDetail>();
    }
}
