namespace Engage.Domain.Entities;

public class ProjectProjectTagEmployeeJobTitle : ProjectProjectTag
{
    public int EmployeeJobTitleId { get; set; }
    public EmployeeJobTitle EmployeeJobTitle { get; set; }
}
