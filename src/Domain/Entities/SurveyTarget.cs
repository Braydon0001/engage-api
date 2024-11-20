// auto-generated
namespace Engage.Domain.Entities;

public class SurveyTarget : BaseAuditableEntity
{
    public int SurveyTargetId { get; set; }
    public int SurveyId { get; set; }

    // Navigation Properties

    public Survey Survey { get; set; }
}