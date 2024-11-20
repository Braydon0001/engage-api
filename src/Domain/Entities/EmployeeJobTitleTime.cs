namespace Engage.Domain.Entities;

public class EmployeeJobTitleTime : BaseAuditableEntity
{
    public int EmployeeJobTitleTimeId { get; set; }
    public int EmployeeJobTitleId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public EmployeeJobTitle EmployeeJobTitle { get; set; }
}