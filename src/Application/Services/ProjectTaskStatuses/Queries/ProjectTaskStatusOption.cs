namespace Engage.Application.Services.ProjectTaskStatuses.Queries;

public class ProjectTaskStatusOption : IMapFrom<ProjectTaskStatus>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskStatus, ProjectTaskStatusOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectTaskStatusId));
    }
}