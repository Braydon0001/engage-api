// auto-generated
namespace Engage.Domain.Entities;

public class SupplierContractDetail : BaseAuditableEntity
{
    public int SupplierContractDetailId { get; set; }
    public int SupplierContractId { get; set; }
    public int SupplierContractDetailTypeId { get; set; }
    public int? EngageRegionId { get; set; }
    public string Name { get; set; }
    public float Amount { get; set; }
    public float? RangeStartAmount { get; set; }
    public float? RangeEndAmount { get; set; }
    public string GlCode { get; set; }
    public string GlSubCode { get; set; }
    public string Note { get; set; }
    public string Reference1 { get; set; }
    public List<JsonFile> Files { get; set; }

    // Navigation Properties

    public SupplierContract SupplierContract { get; set; }
    public SupplierContractDetailType SupplierContractDetailType { get; set; }
    public EngageRegion EngageRegion { get; set; }
}