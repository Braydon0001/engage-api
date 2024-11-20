namespace Engage.Domain.Entities;

public class CommunicationType : BaseAuditableEntity
{
    public int CommunicationTypeId { get; set; }
    public string Name { get; set; }
}