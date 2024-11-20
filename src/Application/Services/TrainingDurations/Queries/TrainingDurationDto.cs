namespace Engage.Application.Services.TrainingDurations.Queries;

public class TrainingDurationDto : IMapFrom<TrainingDuration>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<TrainingDuration, TrainingDurationDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.TrainingDurationId));
    }
}
