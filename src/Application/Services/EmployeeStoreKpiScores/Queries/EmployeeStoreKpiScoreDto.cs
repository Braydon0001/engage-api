namespace Engage.Application.Services.EmployeeStoreKpiScores.Queries;

public class EmployeeStoreKpiScoreDto : IMapFrom<EmployeeStoreKpiScore>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public int StoreId { get; set; }
    public string StoreName { get; set; }
    public int EmployeeKpiId { get; set; }
    public string EmployeeKpiName { get; set; }
    public int? EmployeeKpiTierId { get; set; }
    public string EmployeeKpiTierName { get; set; }
    public float Score { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreKpiScore, EmployeeStoreKpiScoreDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeStoreKpiScoreId))
               .ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => $"{s.Employee.FirstName} {s.Employee.LastName} - {s.Employee.Code}"));
    }
}
