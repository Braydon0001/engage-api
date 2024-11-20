namespace Engage.Application.Services.ProjectFileTypes.Queries;

public class ProjectFileTypeDto : IMapFrom<ProjectFileType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectFileType, ProjectFileTypeDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectFileTypeId));
    }
}
