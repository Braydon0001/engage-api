namespace Engage.Domain.Entities;

public class SurveyFormQuestionOption : BaseAuditableEntity
{
    public int SurveyFormQuestionOptionId { get; set; }
    public int SurveyFormQuestionId { get; set; }
    public int SurveyFormOptionId { get; set; }

    // Navigation Properties

    public SurveyFormQuestion SurveyFormQuestion { get; set; }
    public SurveyFormOption SurveyFormOption { get; set; }
}