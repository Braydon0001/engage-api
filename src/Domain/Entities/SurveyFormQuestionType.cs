namespace Engage.Domain.Entities;

public class SurveyFormQuestionType : BaseAuditableEntity
{
    public int SurveyFormQuestionTypeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}