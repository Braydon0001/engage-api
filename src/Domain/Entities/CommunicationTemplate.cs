namespace Engage.Domain.Entities;

public class CommunicationTemplate : BaseAuditableEntity
{
    public int CommunicationTemplateId { get; set; }
    public string Name { get; set; }
    public string ExternalTemplateId { get; set; }
    public string FromName { get; set; }
    public string FromEmailAddress { get; set; }
    public string FromMobileNumber { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public int CommunicationTemplateTypeId { get; set; } //New Project, New Task, Claim Payment etc.
    public int CommunicationTypeId { get; set; } //Email, WhatsApp, SMS, etc.

    // Navigation Properties 
    public CommunicationTemplateType CommunicationTemplateType { get; set; }
    public CommunicationType CommunicationType { get; set; }
}
