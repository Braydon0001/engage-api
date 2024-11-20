namespace Engage.Domain.Entities;

public class CategoryFileStore : CategoryFileTarget
{
    public int StoreId { get; set; }
    public Store Store { get; set; }
}
