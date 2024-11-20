namespace Engage.Application.Services.CommunicationTemplateTypes.Queries;

public class CommunicationTemplateTypeDto : IMapFrom<CommunicationTemplateType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CommunicationTemplateType, CommunicationTemplateTypeDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CommunicationTemplateTypeId));
    }
}
