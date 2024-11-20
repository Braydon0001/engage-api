namespace Engage.Domain.Entities.LinkEntities;

public class EmployeeReport
{
    public int EmployeeId { get; set; }
    public int ReportId { get; set; }

    public Employee Employee { get; set; }
    public Report Report { get; set; }
}
