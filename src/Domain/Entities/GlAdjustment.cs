namespace Engage.Domain.Entities;

public class GLAdjustment : BaseAuditableEntity
{
    public int GLAdjustmentId { get; set; }
    public string Type { get; set; }
    public int GLCode { get; set; }
    public string GLDescription { get; set; }
    public DateTime TransactionDate { get; set; }
    public string DocumentNo { get; set; }
    public double DebitValue { get; set; }
    public double CreditValue { get; set; }
    public string Description { get; set; }
    public string Invoice { get; set; }
    public string Account { get; set; }
    public int SupplierId { get; set; }
    public int GLAdjustmentTypeId { get; set; }
    public int? BudgetYearId { get; set; }
    public int? BudgetPeriodId { get; set; }

    // Navigation Properties
    public Supplier Supplier { get; set; }
    public GLAdjustmentType GLAdjustmentType { get; set; }
    public BudgetYear BudgetYear { get; set; }
    public BudgetPeriod BudgetPeriod { get; set; }
}
