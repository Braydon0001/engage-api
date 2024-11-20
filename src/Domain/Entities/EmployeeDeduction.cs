namespace Engage.Domain.Entities;

public class EmployeeDeduction : BaseAuditableEntity
{
    public int EmployeeDeductionId { get; set; }
    public int DeductionTypeId { get; set; }
    public int DeductionCycleTypeId { get; set; }
    public int EmployeeId { get; set; }
    public DateTime DeductionDate { get; set; }
    public float Amount { get; set; }
    public string Note { get; set; }
    public string Reference { get; set; }

    // Navigation Properties
    public DeductionType DeductionType { get; set; }
    public DeductionCycleType DeductionCycleType { get; set; }
    public Employee Employee { get; set; }
}
