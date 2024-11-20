namespace Engage.Application.Services.EmployeeEmployeeKpis.Commands;

public class EmployeeEmployeeKpiCommand : IMapTo<EmployeeEmployeeKpi>
{
    public int EmployeeId { get; set; }
    public int EmployeeKpiId { get; set; }
    public int? EmployeeKpiTierId { get; set; }
    public int Score { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeEmployeeKpiCommand, EmployeeEmployeeKpi>();
    }
}


