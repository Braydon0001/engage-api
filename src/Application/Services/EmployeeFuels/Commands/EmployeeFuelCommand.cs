namespace Engage.Application.Services.EmployeeFuels.Commands;

public class EmployeeFuelCommand : IMapTo<EmployeeFuel>
{
    public int EmployeeId { get; set; }
    public int EmployeeVehicleId { get; set; }
    public int? EmployeePaymentTypeId { get; set; }
    public int EmployeeFuelExpenseTypeId { get; set; }
    public string TollgateName { get; set; }
    public DateTime FuelDate { get; set; }
    public decimal? Amount { get; set; }
    public float? Litres { get; set; }
    public int? Odometer { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeFuelCommand, EmployeeFuel>();
    }
}
