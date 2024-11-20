namespace Engage.Domain.Entities;

public class ProjectStakeholderStoreContact : ProjectStakeholder
{
    public int StoreContactId { get; set; }
    public StoreContact StoreContact { get; set; }
}
