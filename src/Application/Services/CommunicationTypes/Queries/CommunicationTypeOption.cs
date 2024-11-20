namespace Engage.Application.Services.CommunicationTypes.Queries;

public class CommunicationTypeOption : IMapFrom<CommunicationType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CommunicationType, CommunicationTypeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CommunicationTypeId));
    }
}