namespace Engage.Domain.Entities;

public class WhatsAppHistory : BaseAuditableEntity
{
    public int WhatsAppHistoryId { get; set; }
    public string ToMobileNumber { get; set; }
    public string FromMobileNumber { get; set; }
    public string FromName { get; set; }
    public string Message { get; set; }
    public string ContentVariables { get; set; }
    public string ExternalTemplateId { get; set; }
    public string AttachmentUrls { get; set; }
    public string Error { get; set; }
}
