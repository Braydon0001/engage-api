namespace Engage.Domain.Entities;

public class TrainingPeriod : BaseAuditableEntity
{
    public int TrainingPeriodId { get; set; }
    public int Number { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TrainingYearId { get; set; }

    //Navigation Props
    public TrainingYear TrainingYear { get; set; }
}
