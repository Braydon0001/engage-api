namespace Engage.Application.Services.CommunicationTemplateTypes.Queries;

public class CommunicationTemplateTypeVm : IMapFrom<CommunicationTemplateType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CommunicationTemplateType, CommunicationTemplateTypeVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CommunicationTemplateTypeId));
    }
}
