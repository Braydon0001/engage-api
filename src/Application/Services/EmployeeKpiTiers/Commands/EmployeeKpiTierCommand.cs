namespace Engage.Application.Services.EmployeeKpiTiers.Commands;

public class EmployeeKpiTierCommand : IMapTo<EmployeeKpiTier>
{
    public int Id { get; set; }
    public int EmployeeKpiId { get; set; }
    public string Name { get; set; }
    public int No { get; set; }
    public string Description { get; set; }
    public int Points { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeKpiTierCommand, EmployeeKpiTier>();
    }
}

