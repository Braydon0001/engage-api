namespace Engage.Domain.Entities;

public class EmployeeJobTitleType : BaseAuditableEntity
{
    public int EmployeeJobTitleTypeId { get; set; }
    public int EmployeeJobTitleId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public EmployeeJobTitle EmployeeJobTitle { get; set; }
}