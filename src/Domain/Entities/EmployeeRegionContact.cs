namespace Engage.Domain.Entities;

public class EmployeeRegionContact : BaseAuditableEntity
{
    public int EmployeeRegionContactId { get; set; }
    public int EngageRegionId { get; set; }
    public int EmployeeId { get; set; }
    public string MobilePhone { get; set; }

    public EngageRegion EngageRegion { get; set; }
    public Employee Employee { get; set; }
}
