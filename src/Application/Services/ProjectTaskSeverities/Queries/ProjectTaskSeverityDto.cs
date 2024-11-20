namespace Engage.Application.Services.ProjectTaskSeverities.Queries;

public class ProjectTaskSeverityDto : IMapFrom<ProjectTaskSeverity>
{
    public int ProjectTaskSeverityId { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskSeverity, ProjectTaskSeverityDto>();
    }
}
