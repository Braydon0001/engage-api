namespace Engage.Application.Services.GLAdjustments.Models;

public class GlAdjustmentTableDataDto
{
    public int Id { get; set; }
    public string Type { get; set; }
    public int GLAdjustmentTypeId { get; set; }
    public string ManualAdjustments { get; set; }
    public string UpdatedBy { get; set; }
    public string GLCode { get; set; }
    public string GLDescription { get; set; }
    public DateTime TransactionDate { get; set; }
    public string DocumentNo { get; set; }
    public double NettValue { get; set; }
    public double DebitValue { get; set; }
    public double CreditValue { get; set; }
    public string Description { get; set; }
    public string Invoice { get; set; }
    public string Account { get; set; }
    public string SupplierName { get; set; }
    public int SupplierId { get; set; }
    public int BudgetYearId { get; set; }
    public int BudgetPeriodId { get; set; }
}
