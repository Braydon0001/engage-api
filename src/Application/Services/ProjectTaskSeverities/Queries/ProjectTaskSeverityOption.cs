namespace Engage.Application.Services.ProjectTaskSeverities.Queries;

public class ProjectTaskSeverityOption : IMapFrom<ProjectTaskSeverity>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskSeverity, ProjectTaskSeverityOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectTaskSeverityId)); ;
    }
}