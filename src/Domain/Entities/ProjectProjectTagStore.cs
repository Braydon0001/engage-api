namespace Engage.Domain.Entities;

public class ProjectProjectTagStore : ProjectProjectTag
{
    public int StoreId { get; set; }
    public Store Store { get; set; }
}
