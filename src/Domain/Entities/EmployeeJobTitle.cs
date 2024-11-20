namespace Engage.Domain.Entities;

public class EmployeeJobTitle : BaseAuditableEntity
{
    public EmployeeJobTitle()
    {
        EmployeeEmployeeJobTitles = new HashSet<EmployeeEmployeeJobTitle>();
        EmployeeJobTitleUserGroups = new HashSet<EmployeeJobTitleUserGroup>();
        EmployeeJobTitleTimes = new HashSet<EmployeeJobTitleTime>();
        EmployeeJobTitleTypes = new HashSet<EmployeeJobTitleType>();
    }
    public int EmployeeJobTitleId { get; set; }
    public int? ParentId { get; set; }
    public int Level { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int? Order { get; set; }

    // Navigation Properties 
    public EmployeeJobTitle Parent { get; set; }
    public ICollection<EmployeeEmployeeJobTitle> EmployeeEmployeeJobTitles { get; set; }
    public ICollection<EmployeeJobTitleUserGroup> EmployeeJobTitleUserGroups { get; private set; }
    public ICollection<EmployeeJobTitleTime> EmployeeJobTitleTimes { get; set; }
    public ICollection<EmployeeJobTitleType> EmployeeJobTitleTypes { get; set; }
}
