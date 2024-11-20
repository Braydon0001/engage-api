namespace Engage.Domain.Entities;

public class SurveyFormAnswerOption : BaseAuditableEntity
{
    public int SurveyFormAnswerOptionId { get; set; }
    public int SurveyFormAnswerId { get; set; }
    public int SurveyFormOptionId { get; set; }

    // Navigation Properties

    public SurveyFormAnswer SurveyFormAnswer { get; set; }
    public SurveyFormOption SurveyFormOption { get; set; }
}