namespace Engage.Domain.Entities;

public class ProjectProjectTagUser : ProjectProjectTag
{
    public int UserId { get; set; }
    public User User { get; set; }
}
