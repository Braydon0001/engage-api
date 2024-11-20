namespace Engage.Domain.Entities;

public class ProjectStakeholderExternalUser : ProjectStakeholder
{
    public int ProjectExternalUserId { get; set; }
    public ProjectExternalUser ProjectExternalUser { get; set; }
}
