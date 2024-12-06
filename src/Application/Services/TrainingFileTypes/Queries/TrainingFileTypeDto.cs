namespace Engage.Application.Services.TrainingFileTypes.Queries;

public class TrainingFileTypeDto : IMapFrom<TrainingFileType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<TrainingFileType, TrainingFileTypeDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.TrainingFileTypeId));
    }
}
