namespace Engage.Domain.Entities;

public class SurveyFormSubmission : BaseFormSubmissionEntity
{

    public SurveyFormSubmission()
    {
        SurveyFormAnswers = new HashSet<SurveyFormAnswer>();
    }
    public int SurveyFormSubmissionId { get; set; }
    public int SurveyFormId { get; set; }
    public int? StoreId { get; set; }
    public string SubmissionUuid { get; set; }
    public DateTime StartedDate { get; set; }
    public bool IsComplete { get; set; }
    public DateTime? CompletedDate { get; set; }
    public bool IsAbandoned { get; set; }
    public DateTime? AbandonedDate { get; set; }

    public string Note { get; set; }

    // Navigation Properties
    public SurveyForm SurveyForm { get; set; }
    public Store Store { get; set; }

    public ICollection<SurveyFormAnswer> SurveyFormAnswers { get; set; }
}