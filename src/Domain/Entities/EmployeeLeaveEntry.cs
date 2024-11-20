namespace Engage.Domain.Entities;

public class EmployeeLeaveEntry : BaseAuditableEntity
{
    public int EmployeeLeaveEntryId { get; set; }
    public int EmployeeId { get; set; }
    public int LeaveTypeId { get; set; }
    public DateTime FromDate { get; set; }
    public bool FromHalfDay { get; set; }
    public DateTime ToDate { get; set; }
    public bool ToHalfDay { get; set; }
    public LeaveEntryStatus Status { get; set; }
    public string Comment { get; set; }
    public bool AdjustLeave { get; set; }
    public string ManagerComment { get; set; }
    public bool Processed { get; set; }
    public DateTime ProcessedDate { get; set; }

    // Navigation Properties
    public Employee Employee { get; set; }
    public LeaveType LeaveType { get; set; }

}
