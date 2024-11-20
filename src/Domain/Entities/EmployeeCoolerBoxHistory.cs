namespace Engage.Domain.Entities;

public class EmployeeCoolerBoxHistory : BaseAuditableEntity
{
    public int EmployeeCoolerBoxHistoryId { get; set; }
    public int EmployeeCoolerBoxId { get; set; }
    public int OldEmployeeId { get; set; }
    public int NewEmployeeId { get; set; }

    // Navigation Properties
    public EmployeeCoolerBox EmployeeCoolerBox { get; set; }
    public Employee OldEmployee { get; set; }
    public Employee NewEmployee { get; set; }

}

