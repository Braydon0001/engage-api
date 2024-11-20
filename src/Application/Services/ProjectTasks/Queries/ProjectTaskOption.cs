namespace Engage.Application.Services.ProjectTasks.Queries;

public class ProjectTaskOption : IMapFrom<ProjectTask>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTask, ProjectTaskOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectTaskId));
    }
}