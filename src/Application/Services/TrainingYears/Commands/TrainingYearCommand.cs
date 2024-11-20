namespace Engage.Application.Services.TrainingYears.Commands;

public class TrainingYearCommand : IMapTo<TrainingYear>
{
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<TrainingYearCommand, TrainingYear>();
    }
}
