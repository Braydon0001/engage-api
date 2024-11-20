namespace Engage.Application.Services.EmployeeBankDetails.Models;

public class EmployeeBankDetailVm : IMapFrom<EmployeeBankDetail>
{
    public int Id { get; set; }
    public OptionDto EmployeeId { get; set; }
    public OptionDto BankAccountOwnerId { get; set; }
    public OptionDto BankAccountTypeId { get; set; }
    public OptionDto BankPaymentMethodId { get; set; }
    public OptionDto BankNameId { get; set; }
    public string BranchCode { get; set; }
    public string AccountNumber { get; set; }
    public string AccountHolder { get; set; }
    public string BeneficiaryReference { get; set; }
    public string SwiftCode { get; set; }
    public string RoutingCode { get; set; }
    public bool IsPrimary { get; set; }
    public string Note { get; set; }
    public bool Disabled { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeBankDetail, EmployeeBankDetailVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeBankDetailId))
            .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeId, s.Employee.FirstName + " " + s.Employee.LastName)))
            .ForMember(d => d.BankAccountOwnerId, opt => opt.MapFrom(s => new OptionDto(s.BankAccountOwnerId, s.BankAccountOwner.Name)))
            .ForMember(d => d.BankAccountTypeId, opt => opt.MapFrom(s => new OptionDto(s.BankAccountTypeId, s.BankAccountType.Name)))
            .ForMember(d => d.BankPaymentMethodId, opt => opt.MapFrom(s => new OptionDto(s.BankPaymentMethodId, s.BankPaymentMethod.Name)))
            .ForMember(d => d.BankNameId, opt => opt.MapFrom(s => new OptionDto(s.BankNameId, s.BankName.Name)));
    }
}
