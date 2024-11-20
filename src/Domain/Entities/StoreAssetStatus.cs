namespace Engage.Domain.Entities;

public class StoreAssetStatus : BaseAuditableEntity
{
    public int StoreAssetStatusId { get; set; }
    public string Name { get; set; }
}