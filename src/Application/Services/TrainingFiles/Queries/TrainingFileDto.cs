namespace Engage.Application.Services.TrainingFiles.Queries;

public class TrainingFileDto : IMapFrom<TrainingFile>
{
    public int Id { get; set; }
    public int TrainingId { get; set; }
    public string TrainingName { get; set; }
    public int TrainingFileTypeId { get; set; }
    public string FileTypeName { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<TrainingFile, TrainingFileDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.TrainingFileId))
               .ForMember(d => d.FileTypeName, opt => opt.MapFrom(s => s.TrainingFileType.Name));
    }
}
