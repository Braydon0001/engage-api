namespace Engage.Application.Services.TrainingYears.Models;

public class TrainingYearDto : IMapFrom<TrainingYear>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<TrainingYear, TrainingYearDto>()
            .ForMember(e => e.Id, opt => opt.MapFrom(d => d.TrainingYearId));
    }
}
