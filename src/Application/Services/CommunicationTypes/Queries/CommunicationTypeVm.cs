
namespace Engage.Application.Services.CommunicationTypes.Queries;

public class CommunicationTypeVm : IMapFrom<CommunicationType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CommunicationType, CommunicationTypeVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CommunicationTypeId));
    }
}
