namespace Engage.Domain.Entities;

public class Budget : BaseAuditableEntity
{
    public Budget()
    {
    }

    public int BudgetId { get; set; }
    public int GLAccountId { get; set; }
    public int BudgetTypeId { get; set; }
    public int BudgetYearId { get; set; }
    public int BudgetVersionId { get; set; }
    public int BudgetPeriodId { get; set; }
    public double Value { get; set; }

    // Navigation Properties
    public GLAccount GLAccount { get; set; }
    public BudgetType BudgetType { get; set; }
    public BudgetYear BudgetYear { get; set; }
    public BudgetVersion BudgetVersion { get; set; }
    public BudgetPeriod BudgetPeriod { get; set; }
}
