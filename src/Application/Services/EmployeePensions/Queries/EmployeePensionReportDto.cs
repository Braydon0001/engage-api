namespace Engage.Application.Services.EmployeePensions.Queries;

public class EmployeePensionReportDto : IMapFrom<Employee>, IMapFrom<EmployeePension>
{
    public string EngageRegions { get; set; }
    public string EmpNo { get; set; }
    public string EmployeeName { get; set; }
    public string IdNo { get; set; }
    public string Race { get; set; }
    public string Gender { get; set; }
    public string PensionScheme { get; set; }
    public string PensionCategory { get; set; }
    public string ContributionPercentage { get; set; }
    public string CreatedDate { get; set; }
    public string IsTerminated { get; set; }
    public string Attachments { get; set; }
    public string Status { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeePension, EmployeePensionReportDto>()
             .ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => s.Employee.FirstName + " " + s.Employee.LastName))
             .ForMember(d => d.EmpNo, opt => opt.MapFrom(s => s.Employee.Code))
             .ForMember(d => d.IdNo, opt => opt.MapFrom(s => s.Employee.IdNumber))
             .ForMember(d => d.Race, opt => opt.MapFrom(s => s.Employee.EmployeeRace.Name))
             .ForMember(d => d.Gender, opt => opt.MapFrom(s => s.Employee.EmployeeGender.Name))
             .ForMember(d => d.PensionScheme, opt => opt.MapFrom(s => s.EmployeePensionScheme.Name))
             .ForMember(d => d.PensionCategory, opt => opt.MapFrom(s => s.EmployeePensionCategory.Name))
             .ForMember(d => d.ContributionPercentage, opt => opt.MapFrom(s => s.EmployeePensionContributionPercentage.Name))
             .ForMember(d => d.IsTerminated, opt => opt.MapFrom(s => s.Employee.Disabled ? "YES" : "NO"))
             .ForMember(d => d.Attachments, opt => opt.MapFrom(s => s.Files.Any() ? "YES" : "NO"))
             .ForMember(d => d.Status, opt => opt.MapFrom(s => "Complete"))
             .ForMember(d => d.EngageRegions, opt => opt.MapFrom(s => string.Join(", ", s.Employee.EmployeeRegions.Select(s => s.EngageRegion.Name))));

        profile.CreateMap<Employee, EmployeePensionReportDto>()
             .ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => s.FirstName + " " + s.LastName))
             .ForMember(d => d.EmpNo, opt => opt.MapFrom(s => s.Code))
             .ForMember(d => d.IdNo, opt => opt.MapFrom(s => s.IdNumber))
             .ForMember(d => d.Race, opt => opt.MapFrom(s => s.EmployeeRace.Name))
             .ForMember(d => d.Gender, opt => opt.MapFrom(s => s.EmployeeGender.Name))
             .ForMember(d => d.IsTerminated, opt => opt.MapFrom(s => s.Disabled ? "YES" : "NO"))
             .ForMember(d => d.Attachments, opt => opt.MapFrom(s => "NO"))
             .ForMember(d => d.Status, opt => opt.MapFrom(s => "Incomplete"))
             .ForMember(d => d.CreatedDate, opt => opt.MapFrom(s => ""))
             .ForMember(d => d.EngageRegions, opt => opt.MapFrom(s => string.Join(", ", s.EmployeeRegions.Select(s => s.EngageRegion.Name))));
    }
}