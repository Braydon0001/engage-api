namespace Engage.Application.Services.ProjectTaskPriorities.Queries;

public class ProjectTaskPriorityDto : IMapFrom<ProjectTaskPriority>
{
    public int ProjectTaskPriorityId { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskPriority, ProjectTaskPriorityDto>();
    }
}
