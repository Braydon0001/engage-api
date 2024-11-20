namespace Engage.Domain.Entities.LinkEntities;

public class EmployeeTraining : BaseAuditableEntity
{
    public int EmployeeId { get; set; }
    public int TrainingId { get; set; }
    public decimal DirectCost { get; set; }
    public decimal AdditionalCost { get; set; }
    public decimal TotalCost { get; set; }
    public string Note { get; set; }

    public decimal AccommodationCost { get; set; }
    public decimal CarHireCost { get; set; }
    public decimal CateringCost { get; set; }
    public decimal FlightsCost { get; set; }
    public decimal FuelCost { get; set; }
    public decimal StationeryCost { get; set; }
    public decimal VenueCost { get; set; }
    public decimal OtherCost { get; set; }

    public Employee Employee { get; set; }
    public Training Training { get; set; }
}
