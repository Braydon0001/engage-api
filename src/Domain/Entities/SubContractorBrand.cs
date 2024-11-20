namespace Engage.Domain.Entities;

public class SubContractorBrand : BaseAuditableEntity
{
    public int SubContractorBrandId { get; set; }
    public int? ParentId { get; set; }
    public int SupplierId { get; set; }
    public int EngageBrandId { get; set; }
    public int EngageRegionId { get; set; }

    public Supplier Parent { get; set; }
    public Supplier Supplier { get; set; }
    public EngageBrand EngageBrand { get; set; }
    public EngageRegion EngageRegion { get; set; }
}
