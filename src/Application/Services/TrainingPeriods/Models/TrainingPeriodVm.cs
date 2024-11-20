namespace Engage.Application.Services.TrainingPeriods.Models;

public class TrainingPeriodVm : IMapFrom<TrainingPeriod>
{
    public int Id { get; set; }
    public OptionDto TrainingYearId { get; set; }
    public int Number { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<TrainingPeriod, TrainingPeriodVm>()
            .ForMember(e => e.Id, opt => opt.MapFrom(d => d.TrainingPeriodId))
            .ForMember(e => e.TrainingYearId, opt => opt.MapFrom(d => new OptionDto(d.TrainingYearId, d.TrainingYear.Name)));
    }
}
