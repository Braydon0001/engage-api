namespace Engage.Domain.Entities;

public class EmployeeVehicleHistory : BaseAuditableEntity
{
    public int EmployeeVehicleHistoryId { get; set; }
    public int EmployeeVehicleId { get; set; }
    public int OldEmployeeId { get; set; }
    public int NewEmployeeId { get; set; }

    // Navigation Properties
    public EmployeeVehicle EmployeeVehicle { get; set; }
    public Employee OldEmployee { get; set; }
    public Employee NewEmployee { get; set; }

}

