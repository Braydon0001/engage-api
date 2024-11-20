namespace Engage.Application.Contracts;
public record TerminationEmail() : BaseContract
{
    public int EmployeeId { get; set; }
    public string ManagerName { get; set; }
    public string ManagerEmail { get; set; }
    public string LeaveManagerEmail { get; set; }
    public string TerminationReason { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeeCode { get; set; }
    public DateTime EndDate { get; set; }
    public string TerminatedBy { get; set; }
}
