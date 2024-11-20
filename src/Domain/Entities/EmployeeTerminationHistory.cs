namespace Engage.Domain.Entities;

public class EmployeeTerminationHistory : BaseAuditableEntity
{
    public int EmployeeTerminationHistoryId { get; set; }
    public int EmployeeId { get; set; }
    public int EmployeeTerminationReasonId { get; set; }
    public DateTime TerminationDate { get; set; }

    // Navigation Properties
    public Employee Employee { get; set; }
    public EmployeeTerminationReason EmployeeTerminationReason { get; set; }
}
