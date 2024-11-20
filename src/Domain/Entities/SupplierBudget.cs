// auto-generated
namespace Engage.Domain.Entities;

public class SupplierBudget : BaseAuditableEntity
{
    public int SupplierBudgetId { get; set; }
    public int SupplierBudgetVersionId { get; set; }
    public int SupplierBudgetTypeId { get; set; }
    public int SupplierId { get; set; }
    public int? SupplierContractDetailId { get; set; }
    public int? EngageRegionId { get; set; }
    public float Amount { get; set; }

    // Navigation Properties

    public SupplierBudgetVersion SupplierBudgetVersion { get; set; }
    public SupplierBudgetType SupplierBudgetType { get; set; }
    public Supplier Supplier { get; set; }
    public SupplierContractDetail SupplierContractDetail { get; set; }
    public EngageRegion EngageRegion { get; set; }
}