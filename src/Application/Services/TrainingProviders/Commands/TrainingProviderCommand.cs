namespace Engage.Application.Services.TrainingProviders.Commands;

public class TrainingProviderCommand : IMapTo<TrainingProvider>
{
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<TrainingProviderCommand, TrainingProvider>();
    }
}
