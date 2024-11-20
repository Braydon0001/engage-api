namespace Engage.Application.Services.ProjectStatuses.Queries;

public class ProjectStatusOption : IMapFrom<ProjectStatus>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectStatus, ProjectStatusOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectStatusId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name));
    }
}