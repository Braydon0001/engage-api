namespace Engage.Application.Services.CommunicationTemplates.Queries;

public class CommunicationTemplateDto : IMapFrom<CommunicationTemplate>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string ExternalTemplateId { get; set; }
    public string FromName { get; set; }
    public string FromEmailAddress { get; set; }
    public string FromMobileNumber { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public int CommunicationTemplateTypeId { get; set; }
    public string CommunicationTemplateTypeName { get; set; }
    public int CommunicationTypeId { get; set; }
    public string CommunicationTypeName { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CommunicationTemplate, CommunicationTemplateDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CommunicationTemplateId));
    }
}
