namespace Engage.Application.Services.TrainingFiles.Queries;

public class TrainingFileVm : IMapFrom<TrainingFile>
{
    public int Id { get; set; }
    public OptionDto TrainingId { get; set; }
    public OptionDto TrainingFileTypeId { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<TrainingFile, TrainingFileVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.TrainingFileId))
               .ForMember(d => d.TrainingId, opt => opt.MapFrom(s => new OptionDto(s.TrainingId, s.Training.Name)))
               .ForMember(d => d.TrainingFileTypeId, opt => opt.MapFrom(s => new OptionDto(s.TrainingFileTypeId, s.TrainingFileType.Name)));
    }
}
