namespace Engage.Domain.Entities;

public class SurveyFormQuestionGroupProduct : BaseAuditableEntity
{
    public int SurveyFormQuestionGroupProductId { get; set; }
    public int SurveyFormQuestionGroupId { get; set; }
    public int EngageVariantProductId { get; set; }

    // Navigation Properties

    public SurveyFormQuestionGroup SurveyFormQuestionGroup { get; set; }
    public EngageVariantProduct EngageVariantProduct { get; set; }
}