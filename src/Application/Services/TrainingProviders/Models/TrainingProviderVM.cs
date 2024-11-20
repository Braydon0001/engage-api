namespace Engage.Application.Services.TrainingProviders.Models;

public class TrainingProviderVm : IMapFrom<TrainingProvider>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<TrainingProvider, TrainingProviderVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.TrainingProviderId));
    }
}
