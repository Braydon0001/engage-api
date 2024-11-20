namespace Engage.Domain.Entities.LinkEntities;

public class EmployeeEmployeeHealthCondition
{
    public int EmployeeId { get; set; }
    public int EmployeeHealthConditionId { get; set; }

    public Employee Employee { get; set; }
    public EmployeeHealthCondition EmployeeHealthCondition { get; set; }
}
