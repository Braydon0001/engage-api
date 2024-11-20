namespace Engage.Application.Services.EmployeeKpis.Commands;

public class EmployeeKpiCommand : IMapTo<EmployeeKpi>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int EmployeeKpiTypeId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeKpiCommand, EmployeeKpi>();
    }
}

