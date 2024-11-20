namespace Engage.Application.Services.ProjectSubTypes.Queries;

public class ProjectSubTypeOption : IMapFrom<ProjectSubType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public int ParentId { get; init; }
    public string ParentName { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectSubType, ProjectSubTypeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectSubTypeId))
               .ForMember(d => d.ParentId, opt => opt.MapFrom(s => s.ProjectTypeId))
               .ForMember(d => d.ParentName, opt => opt.MapFrom(s => s.ProjectType.Name));
    }
}