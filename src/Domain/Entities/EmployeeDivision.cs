namespace Engage.Domain.Entities;

public class EmployeeDivision : BaseAuditableEntity
{
    public EmployeeDivision()
    {
        EmployeeEmployeeDivisions = new HashSet<EmployeeEmployeeDivision>();
    }
    public int EmployeeDivisionId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsRihCallCycles { get; set; }
    public ICollection<EmployeeEmployeeDivision> EmployeeEmployeeDivisions { get; set; }
}
