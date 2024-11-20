namespace Engage.Application.Services.TrainingTypes.Models;

public class TrainingTypeDto : IMapFrom<TrainingType>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public bool Disabled { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<TrainingType, TrainingTypeDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.TrainingTypeId));
    }
}
