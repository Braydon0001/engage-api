// auto-generated
namespace Engage.Domain.Entities;

public class SupplierBudgetVersion : BaseAuditableEntity
{
    public int SupplierBudgetVersionId { get; set; }
    public int SupplierPeriodId { get; set; }
    public int SupplierBudgetVersionTypeId { get; set; }

    // Navigation Properties

    public SupplierPeriod SupplierPeriod { get; set; }
    public SupplierBudgetVersionType SupplierBudgetVersionType { get; set; }
}