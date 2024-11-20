namespace Engage.Application.Services.ProjectSubTypes.Queries;

public class ProjectSubTypeDto : IMapFrom<ProjectSubType>
{
    public int Id { get; init; }
    public int ProjectTypeId { get; init; }
    public string ProjectTypeName { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectSubType, ProjectSubTypeDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectSubTypeId));
    }
}
