namespace Engage.Domain.Entities;

public class EntityContactCommunicationType : BaseAuditableEntity
{
    public int EntityContactCommunicationTypeId { get; set; }
    public int EntityContactId { get; set; }
    public int CommunicationTypeId { get; set; }

    // Navigation Properties

    public EntityContact EntityContact { get; set; }
    public CommunicationType CommunicationType { get; set; }
}