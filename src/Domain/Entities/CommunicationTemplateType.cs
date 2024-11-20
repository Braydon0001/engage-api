namespace Engage.Domain.Entities;

public class CommunicationTemplateType : BaseAuditableEntity
{
    public int CommunicationTemplateTypeId { get; set; }
    public string Name { get; set; }
}