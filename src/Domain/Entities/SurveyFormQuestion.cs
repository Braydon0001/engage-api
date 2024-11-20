namespace Engage.Domain.Entities;

public class SurveyFormQuestion : BaseFormQuestionEntity
{
    public SurveyFormQuestion()
    {
        SurveyFormQuestionProducts = new HashSet<SurveyFormQuestionProduct>();
        SurveyFormQuestionOptions = new HashSet<SurveyFormQuestionOption>();
        SurveyFormQuestionReasons = new HashSet<SurveyFormQuestionReason>();
    }
    public int SurveyFormQuestionId { get; set; }
    public int SurveyFormQuestionGroupId { get; set; }
    public int SurveyFormQuestionTypeId { get; set; }
    public bool IsReasonRequired { get; set; }
    public DateTime? MinDateTime { get; set; }
    public DateTime? MaxDateTime { get; set; }

    // Navigation Properties

    public SurveyFormQuestionGroup SurveyFormQuestionGroup { get; set; }
    public SurveyFormQuestionType SurveyFormQuestionType { get; set; }
    public ICollection<SurveyFormQuestionProduct> SurveyFormQuestionProducts { get; set; }
    public ICollection<SurveyFormQuestionOption> SurveyFormQuestionOptions { get; set; }
    public ICollection<SurveyFormQuestionReason> SurveyFormQuestionReasons { get; set; }
}