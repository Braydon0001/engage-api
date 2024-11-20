namespace Engage.Application.Services.EmployeePensions.Queries;

public class EmployeesWithoutPensionReportDto : IMapFrom<Employee>
{
    public string Region { get; set; }
    public string EmpNo { get; set; }
    public string EmployeeName { get; set; }
    public string IdNo { get; set; }
    public string Race { get; set; }
    public string Gender { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Employee, EmployeesWithoutPensionReportDto>()
             .ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => s.FirstName + " " + s.LastName))
             .ForMember(d => d.EmpNo, opt => opt.MapFrom(s => s.Code))
             .ForMember(d => d.IdNo, opt => opt.MapFrom(s => s.IdNumber))
             .ForMember(d => d.Race, opt => opt.MapFrom(s => s.EmployeeRace.Name))
             .ForMember(d => d.Gender, opt => opt.MapFrom(s => s.EmployeeGender.Name))
             .ForMember(d => d.Region, opt => opt.MapFrom(s => string.Join(", ", s.EmployeeRegions.Select(s => s.EngageRegion.Name))));
    }
}