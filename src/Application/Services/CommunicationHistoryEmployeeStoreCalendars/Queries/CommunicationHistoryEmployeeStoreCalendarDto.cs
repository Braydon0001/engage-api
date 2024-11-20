namespace Engage.Application.Services.CommunicationHistoryEmployeeStoreCalendars.Queries;

public class CommunicationHistoryEmployeeStoreCalendarDto : IMapFrom<CommunicationHistoryEmployeeStoreCalendar>
{
    public int Id { get; init; }
    public int EmployeeStoreCalendarId { get; set; }
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
        profile.CreateMap<CommunicationHistoryEmployeeStoreCalendar, CommunicationHistoryEmployeeStoreCalendarDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CommunicationHistoryId))
               .ForMember(d => d.CommunicationTemplateName, opt => opt.MapFrom(s => s.CommunicationTemplate.CommunicationTemplateType.Name));
    }
}
