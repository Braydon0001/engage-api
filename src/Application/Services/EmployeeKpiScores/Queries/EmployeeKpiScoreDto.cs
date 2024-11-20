namespace Engage.Application.Services.EmployeeKpiScores.Queries;

public class EmployeeKpiScoreDto : IMapFrom<EmployeeKpiScore>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public int EmployeeKpiId { get; set; }
    public string EmployeeKpiName { get; set; }
    public int? EmployeeKpiTierId { get; set; }
    public string EmployeeKpiTierName { get; set; }
    public float Score { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeKpiScore, EmployeeKpiScoreDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeKpiScoreId))
               .ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => $"{s.Employee.FirstName} {s.Employee.LastName} - {s.Employee.Code}"));
    }
}
