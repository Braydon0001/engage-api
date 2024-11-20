namespace Engage.Domain.Entities;

public class DCDepartment : BaseAuditableEntity
{
    public DCDepartment()
    {
        DCDepts = new HashSet<DCDept>();
        DCProductClasses = new HashSet<DCProductClass>();
    }

    public int DCDepartmentId { get; set; }
    public string Name { get; set; }

    // Navigation Properties
    public ICollection<DCDept> DCDepts { get; set; }
    public ICollection<DCProductClass> DCProductClasses { get; set; }
}
