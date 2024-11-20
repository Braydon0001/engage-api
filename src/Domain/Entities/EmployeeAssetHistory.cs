namespace Engage.Domain.Entities;

public class EmployeeAssetHistory : BaseAuditableEntity
{
    public int EmployeeAssetHistoryId { get; set; }
    public int EmployeeAssetId { get; set; }
    public int OldEmployeeId { get; set; }
    public int NewEmployeeId { get; set; }

    // Navigation Properties
    public EmployeeAsset EmployeeAsset { get; set; }
    public Employee OldEmployee { get; set; }
    public Employee NewEmployee { get; set; }

}

