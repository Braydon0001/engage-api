namespace Engage.Domain.Entities;

public class ProjectStakeholderUser : ProjectStakeholder
{
    public ProjectStakeholderUser()
    {
        ProjectTaskProjectStakeholderUsers = new HashSet<ProjectTaskProjectStakeholderUser>();
    }
    public int UserId { get; set; }
    public User User { get; set; }
    public ICollection<ProjectTaskProjectStakeholderUser> ProjectTaskProjectStakeholderUsers { get; private set; }
}
