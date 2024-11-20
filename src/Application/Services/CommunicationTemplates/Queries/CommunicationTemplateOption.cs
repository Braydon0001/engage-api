namespace Engage.Application.Services.CommunicationTemplates.Queries;

public class CommunicationTemplateOption : IMapFrom<CommunicationTemplate>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CommunicationTemplate, CommunicationTemplateOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CommunicationTemplateId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name + " - " + s.CommunicationType.Name));
    }
}