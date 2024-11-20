namespace Engage.Domain.Entities;

public class CostCenterDepartment : BaseAuditableEntity
{
    public int CostCenterDepartmentId { get; set; }
    public int CostCenterId { get; set; }
    public int CostDepartmentId { get; set; }

    // Navigation Properties

    public CostCenter CostCenter { get; set; }
    public CostDepartment CostDepartment { get; set; }
}