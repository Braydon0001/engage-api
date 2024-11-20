namespace Engage.Domain.Entities;

public class EmployeeReinstatementHistory : BaseAuditableEntity
{
    public int EmployeeReinstatementHistoryId { get; set; }
    public int EmployeeId { get; set; }
    public int EmployeeReinstatementReasonId { get; set; }
    public DateTime ReinstatementDate { get; set; }

    // Navigation Properties
    public Employee Employee { get; set; }
    public EmployeeReinstatementReason EmployeeReinstatementReason { get; set; }
}
