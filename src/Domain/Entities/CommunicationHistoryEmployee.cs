namespace Engage.Domain.Entities;

public class CommunicationHistoryEmployee : CommunicationHistory
{
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
}
