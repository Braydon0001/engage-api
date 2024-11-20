namespace Engage.Application.Services.ProjectTypes.Queries;

public class ProjectTypeDto : IMapFrom<ProjectType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public bool IsDescriptionRequired { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectType, ProjectTypeDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectTypeId));
    }
}
