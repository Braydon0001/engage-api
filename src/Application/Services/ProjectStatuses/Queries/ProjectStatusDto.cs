namespace Engage.Application.Services.ProjectStatuses.Queries;

public class ProjectStatusDto : IMapFrom<ProjectStatus>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectStatus, ProjectStatusDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectStatusId));
    }
}
