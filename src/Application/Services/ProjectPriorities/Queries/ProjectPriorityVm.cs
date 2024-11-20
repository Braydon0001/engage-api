
namespace Engage.Application.Services.ProjectPriorities.Queries;

public class ProjectPriorityVm : IMapFrom<ProjectPriority>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public bool IsEndDateRequired { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectPriority, ProjectPriorityVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectPriorityId));
    }
}
