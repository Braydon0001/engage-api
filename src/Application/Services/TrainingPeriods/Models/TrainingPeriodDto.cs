namespace Engage.Application.Services.TrainingPeriods.Models;

public class TrainingPeriodDto : IMapFrom<TrainingPeriod>
{
    public int Id { get; set; }
    public int TrainingYearId { get; set; }
    public string TrainingYearName { get; set; }
    public int Number { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<TrainingPeriod, TrainingPeriodDto>()
            .ForMember(e => e.Id, opt => opt.MapFrom(d => d.TrainingPeriodId))
            .ForMember(d => d.TrainingYearName, opt => opt.MapFrom(s => s.TrainingYear.Name));
    }
}
