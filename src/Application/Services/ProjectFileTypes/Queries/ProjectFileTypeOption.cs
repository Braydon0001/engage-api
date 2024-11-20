namespace Engage.Application.Services.ProjectFileTypes.Queries;

public class ProjectFileTypeOption : IMapFrom<ProjectFileType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectFileType, ProjectFileTypeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectFileTypeId));
    }
}