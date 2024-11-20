namespace Engage.Domain.Entities.LinkEntities;

public class EmployeeRegion
{
    public int EmployeeId { get; set; }
    public int EngageRegionId { get; set; }

    public Employee Employee { get; set; }
    public EngageRegion EngageRegion { get; set; }
}
