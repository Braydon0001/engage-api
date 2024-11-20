namespace Engage.Domain.Entities.LinkEntities;

public class EmployeeEmployeeJobTitle
{
    public int EmployeeId { get; set; }
    public int EmployeeJobTitleId { get; set; }
    public bool IsDisabled { get; set; }
    public string Note { get; set; }

    public Employee Employee { get; set; }
    public EmployeeJobTitle EmployeeJobTitle { get; set; }
}
