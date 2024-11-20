namespace Engage.Domain.Entities;

public class ProjectExternalUserCommunicationType : BaseAuditableEntity
{
    public int ProjectExternalUserCommunicationTypeId { get; set; }
    public int ProjectExternalUserId { get; set; }
    public int CommunicationTypeId { get; set; }

    // Navigation Properties

    public ProjectExternalUser ProjectExternalUser { get; set; }
    public CommunicationType CommunicationType { get; set; }
}