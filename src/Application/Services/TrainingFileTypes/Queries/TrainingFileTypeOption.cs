namespace Engage.Application.Services.TrainingFileTypes.Queries;

public class TrainingFileTypeOption : IMapFrom<TrainingFileType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<TrainingFileType, TrainingFileTypeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.TrainingFileTypeId));
    }
}