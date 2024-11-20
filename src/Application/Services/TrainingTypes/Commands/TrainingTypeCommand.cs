namespace Engage.Application.Services.TrainingTypes.Commands;

public class TrainingTypeCommand : IMapTo<TrainingType>
{
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<TrainingTypeCommand, TrainingType>();
    }
}
