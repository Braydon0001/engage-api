namespace Engage.Domain.Entities;

public class CostSubDepartment : BaseAuditableEntity
{
    public int CostSubDepartmentId { get; set; }
    public int CostDepartmentId { get; set; }
    public string Name { get; set; }

    // Navigation Properties

    public CostDepartment CostDepartment { get; set; }
}