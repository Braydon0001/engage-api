namespace Engage.Domain.Entities;

public class EmployeeVehicle : BaseAuditableEntity
{
    public EmployeeVehicle()
    {
        EmployeeVehicleHistories = new HashSet<EmployeeVehicleHistory>();
    }
    public int EmployeeVehicleId { get; set; }
    public int EmployeeId { get; set; }
    public int VehicleTypeId { get; set; }
    public int VehicleBrandId { get; set; }
    public int AssetStatusId { get; set; }
    public int AssetOwnerId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Tracker { get; set; }
    public string Year { get; set; }
    public string RegistrationNumber { get; set; }
    public string Vin { get; set; }
    public string Note { get; set; }
    public DateTime? RecievedDate { get; set; }
    public DateTime? HandedBackDate { get; set; }

    // Navigation Properties
    public Employee Employee { get; set; }
    public VehicleType VehicleType { get; set; }
    public VehicleBrand VehicleBrand { get; set; }
    public AssetStatus AssetStatus { get; set; }
    public AssetOwner AssetOwner { get; set; }
    public ICollection<EmployeeVehicleHistory> EmployeeVehicleHistories { get; set; }
}

