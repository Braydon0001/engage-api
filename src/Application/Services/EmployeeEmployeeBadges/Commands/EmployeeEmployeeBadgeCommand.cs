namespace Engage.Application.Services.EmployeeEmployeeBadges.Commands;

public class EmployeeEmployeeBadgeCommand : IMapTo<EmployeeEmployeeBadge>
{
    public int EmployeeId { get; set; }
    public int EmployeeBadgeId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeEmployeeBadgeCommand, EmployeeEmployeeBadge>();
    }
}


