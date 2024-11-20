namespace Engage.Domain.Entities;

public class ProjectProjectTagOrder : ProjectProjectTag
{
    public int OrderId { get; set; }
    public Order Order { get; set; }
}
