namespace Engage.Domain.Entities;

public class CostCenterEmployee : BaseAuditableEntity
{
    public int CostCenterEmployeeId { get; set; }
    public int CostCenterId { get; set; }
    public int EmployeeId { get; set; }

    // Navigation Properties

    public CostCenter CostCenter { get; set; }
    public Employee Employee { get; set; }
}