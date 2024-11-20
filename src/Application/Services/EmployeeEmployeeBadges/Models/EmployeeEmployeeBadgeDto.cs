namespace Engage.Application.Services.EmployeeEmployeeBadges.Models;
public class EmployeeEmployeeBadgeDto : IMapFrom<EmployeeEmployeeBadge>
{
    public int EmployeeId { get; set; }
    public int EmployeeBadgeId { get; set; }
    public string EmployeeBadgeName { get; set; }
    public string EmployeeBadgeDescription { get; set; }
    public int EmployeeBadgePoints { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeEmployeeBadge, EmployeeEmployeeBadgeDto>()
           .ForMember(d => d.EmployeeBadgePoints, opt => opt.MapFrom(s => s.EmployeeBadge.Points));
    }
}
