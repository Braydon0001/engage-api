namespace Engage.Domain.Entities;

public class SurveyFormAnswer : BaseFormAnswerEntity
{
    public SurveyFormAnswer()
    {
        SurveyFormAnswerOptions = new HashSet<SurveyFormAnswerOption>();
    }
    public int SurveyFormAnswerId { get; set; }
    public int SurveyFormSubmissionId { get; set; }
    public int SurveyFormQuestionId { get; set; }
    public int? SurveyFormReasonId { get; set; }
    public string AnswerUuid { get; set; }

    // Navigation Properties

    public SurveyFormSubmission SurveyFormSubmission { get; set; }
    public SurveyFormQuestion SurveyFormQuestion { get; set; }
    public SurveyFormReason SurveyFormReason { get; set; }
    public ICollection<SurveyFormAnswerOption> SurveyFormAnswerOptions { get; set; }
}