
using Engage.Application.Services.ProjectTypes.Queries;

namespace Engage.Application.Services.ProjectSubTypes.Queries;

public class ProjectSubTypeVm : IMapFrom<ProjectSubType>
{
    public int Id { get; init; }
    public ProjectTypeOption ProjectTypeId { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectSubType, ProjectSubTypeVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectSubTypeId))
               .ForMember(d => d.ProjectTypeId, opt => opt.MapFrom(s => s.ProjectType));
    }
}
