namespace Engage.Application.Services.ProjectTaskStatuses.Queries;

public class ProjectTaskStatusDto : IMapFrom<ProjectTaskStatus>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskStatus, ProjectTaskStatusDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectTaskStatusId));
    }
}
