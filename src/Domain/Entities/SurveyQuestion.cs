namespace Engage.Domain.Entities;

public class SurveyQuestion : BaseAuditableEntity
{
    public SurveyQuestion()
    {
        SurveyQuestionOptions = new HashSet<SurveyQuestionOption>();
        SurveyQuestionFalseReasons = new HashSet<SurveyQuestionFalseReason>();
        EmployeeStoreSurveyAnswers = new HashSet<SurveyAnswer>();
        Rules = new HashSet<SurveyQuestionRule>();
    }

    public int SurveyQuestionId { get; set; }
    public int SurveyId { get; set; }
    public int QuestionTypeId { get; set; }
    public int? EngageVariantProductId { get; set; }
    public int? StoreConceptId { get; set; }
    public int? StoreConceptAttributeId { get; set; }
    public string Question { get; set; }
    public int? DisplayOrder { get; set; }
    public bool IsRequired { get; set; }
    public bool IsFalseOptionRequired { get; set; }

    // Navigation Properties
    public Survey Survey { get; set; }
    public QuestionType QuestionType { get; set; }
    public EngageVariantProduct EngageVariantProduct { get; set; }
    public StoreConcept StoreConcept { get; set; }
    public StoreConceptAttribute StoreConceptAttribute { get; set; }
    public ICollection<SurveyQuestionOption> SurveyQuestionOptions { get; set; }
    public ICollection<SurveyQuestionFalseReason> SurveyQuestionFalseReasons { get; set; }
    public ICollection<SurveyAnswer> EmployeeStoreSurveyAnswers { get; set; }
    public ICollection<SurveyQuestionRule> Rules { get; set; }
}
