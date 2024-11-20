namespace Engage.Application.Services.ProjectFiles.Queries;

public class ProjectFileDto : IMapFrom<ProjectFile>
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string ProjectName { get; set; }
    public int ProjectFileTypeId { get; set; }
    public string FileTypeName { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectFile, ProjectFileDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectFileId))
               .ForMember(d => d.FileTypeName, opt => opt.MapFrom(s => s.ProjectFileType.Name));
    }
}
