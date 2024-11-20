namespace Engage.Application.Services.CommunicationHistoryClaimFloats.Queries;

public class CommunicationHistoryClaimFloatDto : IMapFrom<CommunicationHistoryClaimFloat>
{
    public int Id { get; init; }
    public int ClaimFloatId { get; set; }
    public int CommunicationTemplateId { get; set; }
    public string CommunicationTemplateName { get; set; }
    public string ToEmail { get; set; }
    public string FromEmail { get; set; }
    public string FromName { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public string CcEmails { get; set; }
    public string AttachmentUrls { get; set; }
    public bool HasMemoryStreamAttachment { get; set; }
    public string Error { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CommunicationHistoryClaimFloat, CommunicationHistoryClaimFloatDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CommunicationHistoryId))
               .ForMember(d => d.CommunicationTemplateName, opt => opt.MapFrom(s => s.CommunicationTemplate.CommunicationTemplateType.Name));
    }
}
