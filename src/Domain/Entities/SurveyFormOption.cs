namespace Engage.Domain.Entities;

public class SurveyFormOption : BaseAuditableEntity
{
    public int SurveyFormOptionId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool CompleteSurvey { get; set; }
}