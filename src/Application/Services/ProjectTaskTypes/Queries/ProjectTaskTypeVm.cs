
namespace Engage.Application.Services.ProjectTaskTypes.Queries;

public class ProjectTaskTypeVm : IMapFrom<ProjectTaskType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskType, ProjectTaskTypeVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectTaskTypeId));
    }
}
