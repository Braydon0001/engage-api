namespace Engage.Application.Services.EmployeeBadges.Models;
using Engage.Application.Services.Employees.Models;
public class EmployeeBadgeDto : IMapFrom<EmployeeBadge>
{
    public int Id { get; set; }
    public string EmployeeBadgeTypeName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Points { get; set; }
    public List<EmployeeEmployeeBadge> EmployeeBadges { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeBadge, EmployeeBadgeDto>()
           .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeBadgeId))
           .ForMember(d => d.EmployeeBadges, opt => opt.MapFrom(s => s.EmployeeBadges.Select(o =>
                                                                            new EmployeeEmployeeBadge(o.EmployeeId, o.EmployeeBadgeId))));
    }
}
