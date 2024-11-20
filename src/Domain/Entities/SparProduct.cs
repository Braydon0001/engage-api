namespace Engage.Domain.Entities;

public class SparProduct : BaseAuditableEntity
{
    public int SparProductId { get; set; }
    public string ItemCode { get; set; }
    public string Name { get; set; }
    public float? UnitSize { get; set; }
    public int? SparUnitTypeId { get; set; }
    public string Barcode { get; set; }
    public int EngageBrandId { get; set; }
    public int SupplierId { get; set; }
    public int EngageSubCategoryId { get; set; }
    public int SparProductStatusId { get; set; }
    public int SparAnalysisGroupId { get; set; }
    public int SparSystemStatusId { get; set; }
    public int EvoLedgerId { get; set; }
    public List<JsonFile> Files { get; set; }

    // Navigation Properties

    public SparUnitType SparUnitType { get; set; }
    public EngageBrand EngageBrand { get; set; }
    public Supplier Supplier { get; set; }
    public EngageSubCategory EngageSubCategory { get; set; }
    public SparProductStatus SparProductStatus { get; set; }
    public SparAnalysisGroup SparAnalysisGroup { get; set; }
    public SparSystemStatus SparSystemStatus { get; set; }
    public EvoLedger EvoLedger { get; set; }
}