namespace Engage.Domain.Entities;

public class SurveyFormProduct : BaseAuditableEntity
{
    public int SurveyFormProductId { get; set; }
    public int SurveyFormId { get; set; }
    public int EngageMasterProductId { get; set; }

    // Navigation Properties

    public SurveyForm SurveyForm { get; set; }
    public EngageMasterProduct EngageMasterProduct { get; set; }
}