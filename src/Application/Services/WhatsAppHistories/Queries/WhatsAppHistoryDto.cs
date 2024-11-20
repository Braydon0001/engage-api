namespace Engage.Application.Services.WhatsAppHistories.Queries;

public class WhatsAppHistoryDto : IMapFrom<WhatsAppHistory>
{
    public int Id { get; init; }
    public string ToMobileNumber { get; set; }
    public string FromMobileNumber { get; set; }
    public string FromName { get; set; }
    public string Message { get; set; }
    public string ContentVariables { get; set; }
    public string ExternalTemplateId { get; set; }
    public string AttachmentUrls { get; set; }
    public string Error { get; set; }
    public DateTime Created { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<WhatsAppHistory, WhatsAppHistoryDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.WhatsAppHistoryId));
    }
}
