namespace Engage.Domain.Entities;

public class TrainingYear : BaseAuditableEntity
{
    public TrainingYear()
    {
        TrainingPeriods = new HashSet<TrainingPeriod>();
    }
    //Required
    public int TrainingYearId { get; set; }
    public string Name { get; set; }

    public ICollection<TrainingPeriod> TrainingPeriods { get; set; }
}
