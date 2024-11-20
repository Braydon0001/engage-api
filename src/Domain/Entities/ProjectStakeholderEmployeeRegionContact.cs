namespace Engage.Domain.Entities;

public class ProjectStakeholderEmployeeRegionContact : ProjectStakeholder
{
    public int EmployeeRegionContactId { get; set; }
    public EmployeeRegionContact EmployeeRegionContact { get; set; }
}
