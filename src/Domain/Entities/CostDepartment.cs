namespace Engage.Domain.Entities;

public class CostDepartment : BaseAuditableEntity
{
    public CostDepartment()
    {
        CostSubDepartments = new HashSet<CostSubDepartment>();
    }
    public int CostDepartmentId { get; set; }
    public string Name { get; set; }

    public ICollection<CostSubDepartment> CostSubDepartments { get; set; }
}