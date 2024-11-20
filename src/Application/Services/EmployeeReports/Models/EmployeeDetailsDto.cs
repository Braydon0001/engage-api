namespace Engage.Application.Services.EmployeeReports.Models;
public class EmployeeDetailsDto : IMapFrom<Employee>
{
    public string EmployeeNumber { get; set; }                     //A
    public string Title { get; set; }                              //B
    public string FirstName { get; set; }                          //C
    public string MiddleName { get; set; }                         //D
    public string LastName { get; set; }                           //E
    public string Initials { get; set; }                           //F
    public string HomeNumber { get; set; }                         //G
    public string WorkNumber { get; set; }                         //H
    public string PhoneNumber { get; set; }                        //I
    public string WorkExtension { get; set; }                      //J
    public string EmailAddress1 { get; set; }                      //K
    public string EmployeeLanguageName { get; set; }               //L
    public string Gender { get; set; }                             //M
    public string Race { get; set; }                               //N
    public string Nationality { get; set; }                        //O
    public string Citizenship { get; set; }                        //P
    public string EmployeeDisabledTypeName { get; set; }           //Q
    public string EmployeeUIFExemptionName { get; set; }           //R
    public string EmployeeSDLExemptionName { get; set; }           //S
    public string DateOfBirth { get; set; }                        //T
    public string DefaultPayslip { get; set; }                     //U
    public string CustomField { get; set; }                        //V
    public string KnownAs { get; set; }                            //W
    public string MaritalStatus { get; set; }                      //X
    public string Retired { get; set; }                            //Y
    public string NextOfKinName { get; set; }                      //Z
    public string NextOfKinContactNumber { get; set; }             //AA
    public string NextOfKinAddess { get; set; }                    //AB

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Employee, EmployeeDetailsDto>()
                .ForMember(d => d.EmployeeNumber, opt => opt.MapFrom(s => s.Code))
                .ForMember(d => d.EmployeeLanguageName, opt => opt.MapFrom(s => s.EmployeeLanguage.Name))
                .ForMember(d => d.Nationality, opt => opt.MapFrom(s => s.EmployeeNationality.Name))
                .ForMember(d => d.Citizenship, opt => opt.MapFrom(s => s.EmployeeCitzenship.Name))
                .ForMember(d => d.EmployeeDisabledTypeName, opt => opt.MapFrom(s => s.EmployeeDisabledType.Name))
                .ForMember(d => d.EmployeeUIFExemptionName, opt => opt.MapFrom(s => s.EmployeeUIFExemption.Name))
                .ForMember(d => d.EmployeeSDLExemptionName, opt => opt.MapFrom(s => s.EmployeeSDLExemption.Name))
                .ForMember(d => d.DateOfBirth, opt => opt.MapFrom(s => s.DateOfBirth.ToString("yyyy/MM/dd")))
                .ForMember(d => d.DefaultPayslip, opt => opt.MapFrom(s => s.EmployeeDefaultPayslip.Name))
                .ForMember(d => d.Retired, opt => opt.MapFrom(s => s.IsRetired ? "True" : "False"));
    }
}
