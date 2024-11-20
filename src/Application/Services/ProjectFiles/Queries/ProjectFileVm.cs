namespace Engage.Application.Services.ProjectFiles.Queries;

public class ProjectFileVm : IMapFrom<ProjectFile>
{
    public int Id { get; set; }
    public OptionDto ProjectId { get; set; }
    public OptionDto ProjectFileTypeId { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectFile, ProjectFileVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectFileId))
               .ForMember(d => d.ProjectId, opt => opt.MapFrom(s => new OptionDto(s.ProjectId, s.Project.Name)))
               .ForMember(d => d.ProjectFileTypeId, opt => opt.MapFrom(s => new OptionDto(s.ProjectFileTypeId, s.ProjectFileType.Name)));
    }
}
