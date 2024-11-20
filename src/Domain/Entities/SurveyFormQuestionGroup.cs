namespace Engage.Domain.Entities;

public class SurveyFormQuestionGroup : BaseFormQuestionGroupEntity
{
    public SurveyFormQuestionGroup()
    {
        SurveyFormQuestions = new HashSet<SurveyFormQuestion>();
        SurveyFormQuestionGroupProducts = new HashSet<SurveyFormQuestionGroupProduct>();
    }
    public int SurveyFormQuestionGroupId { get; set; }
    public int SurveyFormId { get; set; }
    //TODO: Remove this property
    public bool IsVirtualGroup { get; set; }
    //TODO: Remove this property
    public float? CategoryTargetValue { get; set; }

    // Navigation Properties

    public SurveyForm SurveyForm { get; set; }
    public ICollection<SurveyFormQuestion> SurveyFormQuestions { get; set; }
    public ICollection<SurveyFormQuestionGroupProduct> SurveyFormQuestionGroupProducts { get; set; }
}