namespace Engage.Application.Services.EmployeeReports.Models;
public class EmployeeBankDetailsDto : IMapFrom<EmployeeBankDetail>
{
    public string EmployeeNumber { get; set; }                     //A
    public string PaymentMethod { get; set; }                      //B
    public string BankAccountOwner { get; set; }                   //C
    public string BankAccountOwnerName { get; set; }               //D
    public string AccountType { get; set; }                        //E
    public string BankName { get; set; }                           //F
    public string BranchNo { get; set; }                           //G
    public string AccountNo { get; set; }                          //H
    public string BeneficiaryReference { get; set; }               //I
    public string Comments { get; set; }                           //J
    public string SwiftCode { get; set; }                          //K
    public string RoutingCode { get; set; }                        //L
    public string Indicator { get; set; }                          //M
    public string SplitMethod { get; set; }                        //N
    public string Amount { get; set; }                             //O
    public string PayslipComponent { get; set; }                   //P
    public string Currency { get; set; }                           //Q

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeBankDetail, EmployeeBankDetailsDto>()
                .ForMember(d => d.EmployeeNumber, opt => opt.MapFrom(s => s.Employee.Code))
                .ForMember(d => d.PaymentMethod, opt => opt.MapFrom(s => s.BankPaymentMethod.Name))
                .ForMember(d => d.BankAccountOwner, opt => opt.MapFrom(s => s.BankAccountOwner.Name))
                .ForMember(d => d.BankAccountOwnerName, opt => opt.MapFrom(s => s.Employee.FirstName +" "+s.Employee.LastName))
                .ForMember(d => d.AccountType, opt => opt.MapFrom(s => s.BankAccountType.Name))
                .ForMember(d => d.BankName, opt => opt.MapFrom(s => s.BankName.Name))
                .ForMember(d => d.BranchNo, opt => opt.MapFrom(s => s.BranchCode))
                .ForMember(d => d.AccountNo, opt => opt.MapFrom(s => s.AccountNumber))
                .ForMember(d => d.BeneficiaryReference, opt => opt.MapFrom(s => s.BeneficiaryReference))
                .ForMember(d => d.Comments, opt => opt.MapFrom(s => s.Note))
                .ForMember(d => d.SwiftCode, opt => opt.MapFrom(s => s.SwiftCode))
                .ForMember(d => d.RoutingCode, opt => opt.MapFrom(s => s.RoutingCode))
                .ForMember(d => d.Indicator, opt => opt.MapFrom(s => "-"))
                .ForMember(d => d.SplitMethod, opt => opt.MapFrom(s => "-"))
                .ForMember(d => d.PayslipComponent, opt => opt.MapFrom(s => "-"))
                .ForMember(d => d.Currency, opt => opt.MapFrom(s => "-"));
    }
}
