namespace Engage.Domain.Entities;

public class ProjectStore : Project
{
    public int StoreId { get; set; }
    public Store Store { get; set; }
}
