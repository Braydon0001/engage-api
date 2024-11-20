namespace Engage.Domain.Entities;

public class ProjectExternalUser : BaseAuditableEntity
{
    public ProjectExternalUser()
    {
        CommunicationTypes = new HashSet<ProjectExternalUserCommunicationType>();
    }
    public int ProjectExternalUserId { get; set; }
    public int? ExternalUserTypeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string CellNumber { get; set; }
    public ExternalUserType ExternalUserType { get; set; }
    public ICollection<ProjectExternalUserCommunicationType> CommunicationTypes { get; set; }
}