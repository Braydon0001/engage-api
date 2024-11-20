namespace Engage.Domain.Entities;

public class CostCenter : BaseAuditableEntity
{
    public CostCenter()
    {
        CostCenterDepartments = new HashSet<CostCenterDepartment>();
        CostCenterEmployees = new HashSet<CostCenterEmployee>();
    }
    public int CostCenterId { get; set; }
    public int CostTypeId { get; set; }
    public string Name { get; set; }

    // Navigation Properties

    public CostType CostType { get; set; }
    public ICollection<CostCenterDepartment> CostCenterDepartments { get; set; }
    public ICollection<CostCenterEmployee> CostCenterEmployees { get; set; }
}