
namespace Engage.Application.Services.ProjectTaskStatuses.Queries;

public class ProjectTaskStatusVm : IMapFrom<ProjectTaskStatus>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskStatus, ProjectTaskStatusVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectTaskStatusId));
    }
}
