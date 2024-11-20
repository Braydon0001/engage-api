// auto-generated
namespace Engage.Domain.Entities;

public class StoreOwner : BaseAuditableEntity
{
    public int StoreOwnerId { get; set; }
    public int StoreId { get; set; }
    public int StoreGroupId { get; set; }
    public int StoreOwnerTypeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Note { get; set; }
    public string Name { get; set; }
    public bool IsPrimaryOwner { get; set; }

    // Navigation Properties

    public Store Store { get; set; }
    public StoreGroup StoreGroup { get; set; }
    public StoreOwnerType StoreOwnerType { get; set; }
}