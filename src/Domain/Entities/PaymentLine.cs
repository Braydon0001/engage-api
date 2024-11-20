namespace Engage.Domain.Entities;

public class PaymentLine : BaseAuditableEntity
{
    public PaymentLine()
    {
        Employees = new HashSet<PaymentLineEmployee>();
        CostCenters = new HashSet<PaymentLineCostCenter>();
        EmployeeDivisions = new HashSet<PaymentLineDivision>();
        SubDepartments = new HashSet<PaymentLineCostSubDepartment>();
    }
    public int PaymentLineId { get; set; }
    public int PaymentId { get; set; }
    public int ExpenseTypeId { get; set; }
    public int? VatId { get; set; }
    public float Amount { get; set; }
    public float VatAmount { get; set; }
    public int? Quantity { get; set; }
    public bool IsVat { get; set; }
    public bool IsSplitAmount { get; set; }
    public bool HasQuote { get; set; }
    public bool HasInvoice { get; set; }
    public string Note { get; set; }
    public List<JsonFile> Files { get; set; }

    // Navigation Properties

    public Payment Payment { get; set; }
    public ExpenseType ExpenseType { get; set; }
    public Vat Vat { get; set; }

    public ICollection<PaymentLineEmployee> Employees { get; set; }
    public ICollection<PaymentLineCostCenter> CostCenters { get; set; }
    public ICollection<PaymentLineDivision> EmployeeDivisions { get; set; }
    public ICollection<PaymentLineCostSubDepartment> SubDepartments { get; set; }
}