namespace Engage.Application.Services.EmployeeBadges.Commands;

public class EmployeeBadgeCommand : IMapTo<EmployeeBadge>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Points { get; set; }
    public int EmployeeBadgeTypeId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeBadgeCommand, EmployeeBadge>();
    }
}

