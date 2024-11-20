namespace Engage.Application.Services.EmployeeHrStatistics.Model;

public class DepartmentGroupHeadCountDto
{
    public DedicatedDepartmentGroupHeadCountDto DedicatedDepartmentGroupEmployees { get; set; }
    public List<StatCardDto> DepartmentGroupHeadCounts { get; set; }
}

public class DedicatedDepartmentGroupHeadCountDto
{
    public StatCardDto SingleRoleHeadCount { get; set; }
    public StatCardDto MultiRoleHeadCount { get; set; }
}