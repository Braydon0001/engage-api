namespace Engage.Domain.Entities.LinkEntities;

public class TrainingFacilitator : BaseAuditableEntity
{
    public int TrainingId { get; set; }
    public int EmployeeId { get; set; }
    public decimal DirectCost { get; set; }
    public decimal AdditionalCost { get; set; }
    public decimal TotalCost { get; set; }
    public string Note { get; set; }

    public Employee Employee { get; set; }
    public Training Training { get; set; }
}
