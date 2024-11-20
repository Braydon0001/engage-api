namespace Engage.Application.Services.TrainingTypes.Models;

public class TrainingTypeVm : IMapFrom<TrainingType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<TrainingType, TrainingTypeVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.TrainingTypeId));
    }
}
