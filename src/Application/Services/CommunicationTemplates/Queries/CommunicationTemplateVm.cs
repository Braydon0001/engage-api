
using Engage.Application.Services.CommunicationTemplateTypes.Queries;
using Engage.Application.Services.CommunicationTypes.Queries;

namespace Engage.Application.Services.CommunicationTemplates.Queries;

public class CommunicationTemplateVm : IMapFrom<CommunicationTemplate>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string ExternalTemplateId { get; set; }
    public string FromName { get; set; }
    public string FromEmailAddress { get; set; }
    public string FromMobileNumber { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public CommunicationTemplateTypeOption CommunicationTemplateTypeId { get; set; }
    public CommunicationTypeOption CommunicationTypeId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CommunicationTemplate, CommunicationTemplateVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CommunicationTemplateId))
               .ForMember(d => d.CommunicationTemplateTypeId, opt => opt.MapFrom(s => s.CommunicationTemplateType))
               .ForMember(d => d.CommunicationTypeId, opt => opt.MapFrom(s => s.CommunicationType));
    }
}
