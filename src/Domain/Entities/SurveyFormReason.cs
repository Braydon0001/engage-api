namespace Engage.Domain.Entities;

public class SurveyFormReason : BaseAuditableEntity
{
    public int SurveyFormReasonId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool CompleteSurvey { get; set; }
}