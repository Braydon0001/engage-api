namespace Engage.Domain.Entities;

public class EmployeeFuel : BaseAuditableEntity
{
    public int EmployeeFuelId { get; set; }
    public int EmployeeId { get; set; }
    public int EmployeeVehicleId { get; set; }
    public int? EmployeePaymentTypeId { get; set; }
    public int EmployeeFuelExpenseTypeId { get; set; }
    public DateTime FuelDate { get; set; }
    public decimal? Amount { get; set; }
    public float? Litres { get; set; }
    public int? Odometer { get; set; }
    public string TollgateName { get; set; }
    public string BlobUrl { get; set; }
    public string BlobName { get; set; }

    // Navigation Properties

    public Employee Employee { get; set; }
    public EmployeeVehicle EmployeeVehicle { get; set; }
    public EmployeeFuelExpenseType EmployeeFuelExpenseType { get; set; }
    public EmployeePaymentType EmployeePaymentType { get; set; }

}
