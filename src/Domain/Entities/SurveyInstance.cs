namespace Engage.Domain.Entities;

public class SurveyInstance : BaseAuditableEntity
{
    public SurveyInstance()
    {
        SurveyAnswers = new HashSet<SurveyAnswer>();
    }

    public int SurveyInstanceId { get; set; }
    public int EmployeeId { get; set; }
    public int StoreId { get; set; }
    public int SurveyId { get; set; }
    public string Note { get; set; }
    public DateTime SurveyDate { get; set; }
    public bool IsCompleted { get; set; }

    // Navigation Properties
    public Employee Employee { get; set; }
    public Store Store { get; set; }
    public Survey Survey { get; set; }

    public ICollection<SurveyAnswer> SurveyAnswers { get; set; }
}
