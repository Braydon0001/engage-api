namespace Engage.Domain.Entities;

public class UserCommunicationType : BaseAuditableEntity
{
    public int UserCommunicationTypeId { get; set; }
    public int UserId { get; set; }
    public int CommunicationTypeId { get; set; }

    // Navigation Properties

    public User User { get; set; }
    public CommunicationType CommunicationType { get; set; }
}