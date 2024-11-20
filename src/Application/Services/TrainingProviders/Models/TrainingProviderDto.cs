namespace Engage.Application.Services.TrainingProviders.Models;

public class TrainingProviderDto : IMapFrom<TrainingProvider>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public bool Disabled { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<TrainingProvider, TrainingProviderDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.TrainingProviderId));
    }
}
