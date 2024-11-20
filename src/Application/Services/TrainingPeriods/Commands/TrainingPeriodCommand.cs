namespace Engage.Application.Services.TrainingPeriods.Commands;

public class TrainingPeriodCommand : IMapTo<TrainingPeriod>
{
    public int TrainingYearId { get; set; }
    public int TrainingId { get; set; }
    public int Number { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<TrainingPeriodCommand, TrainingPeriod>();
    }
}
