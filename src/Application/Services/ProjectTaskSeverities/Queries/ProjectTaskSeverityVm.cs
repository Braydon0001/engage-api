namespace Engage.Application.Services.ProjectTaskSeverities.Queries;

public class ProjectTaskSeverityVm : IMapFrom<ProjectTaskSeverity>
{
    public int ProjectTaskSeverityId { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskSeverity, ProjectTaskSeverityVm>();
    }
}
