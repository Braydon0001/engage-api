namespace Engage.Domain.Entities;

public class EmployeeCoolerBox : BaseAuditableEntity
{
    public EmployeeCoolerBox()
    {
        EmployeeCoolerBoxHistories = new HashSet<EmployeeCoolerBoxHistory>();
    }
    public int EmployeeCoolerBoxId { get; set; }
    public int EmployeeId { get; set; }
    public int EmployeeCoolerBoxConditionId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Note { get; set; }
    public DateTime? RecievedDate { get; set; }
    public DateTime? HandedBackDate { get; set; }
    public List<JsonFile> Files { get; set; }

    // Navigation Properties
    public Employee Employee { get; set; }
    public EmployeeCoolerBoxCondition EmployeeCoolerBoxCondition { get; set; }
    public ICollection<EmployeeCoolerBoxHistory> EmployeeCoolerBoxHistories { get; set; }
}

