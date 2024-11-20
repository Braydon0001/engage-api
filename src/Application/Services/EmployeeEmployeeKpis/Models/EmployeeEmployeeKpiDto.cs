namespace Engage.Application.Services.EmployeeEmployeeKpis.Models;
public class EmployeeEmployeeKpiDto : IMapFrom<EmployeeEmployeeKpi>
{
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public int EmployeeKpiId { get; set; }
    public string EmployeeKpiName { get; set; }
    public int? EmployeeKpiTierId { get; set; }
    public string EmployeeKpiTierName { get; set; }
    public int Score { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeEmployeeKpi, EmployeeEmployeeKpiDto>()
               .ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => $"{s.Employee.FirstName} {s.Employee.LastName} - {s.Employee.Code}"));
    }
}
