namespace Engage.Domain.Entities;

public class EmployeeHealthCondition : BaseAuditableEntity
{
    public EmployeeHealthCondition()
    {
        EmployeeEmployeeHealthConditions = new HashSet<EmployeeEmployeeHealthCondition>();
    }
    public int EmployeeHealthConditionId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<EmployeeEmployeeHealthCondition> EmployeeEmployeeHealthConditions { get; set; }
}