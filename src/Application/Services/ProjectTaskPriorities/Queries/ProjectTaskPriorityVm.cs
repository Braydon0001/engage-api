
namespace Engage.Application.Services.ProjectTaskPriorities.Queries;

public class ProjectTaskPriorityVm : IMapFrom<ProjectTaskPriority>
{
    public int ProjectTaskPriorityId { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskPriority, ProjectTaskPriorityVm>();
    }
}
