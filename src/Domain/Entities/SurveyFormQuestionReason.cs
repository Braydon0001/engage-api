namespace Engage.Domain.Entities;

public class SurveyFormQuestionReason : BaseAuditableEntity
{
    public int SurveyFormQuestionReasonId { get; set; }
    public int SurveyFormQuestionId { get; set; }
    public int SurveyFormReasonId { get; set; }

    // Navigation Properties

    public SurveyFormQuestion SurveyFormQuestion { get; set; }
    public SurveyFormReason SurveyFormReason { get; set; }
}