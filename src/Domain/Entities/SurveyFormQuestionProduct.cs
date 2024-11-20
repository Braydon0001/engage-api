namespace Engage.Domain.Entities;

public class SurveyFormQuestionProduct : BaseAuditableEntity
{
    public int SurveyFormQuestionProductId { get; set; }
    public int SurveyFormQuestionId { get; set; }
    public int EngageVariantProductId { get; set; }

    // Navigation Properties

    public SurveyFormQuestion SurveyFormQuestion { get; set; }
    public EngageVariantProduct EngageVariantProduct { get; set; }
}