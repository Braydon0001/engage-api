namespace Engage.Domain.Entities
{
    public class SurveyFormTarget : BaseAuditableEntity
    {
        public int SurveyFormTargetId { get; set; }
        public int SurveyFormId { get; set; }

        // Navigation Properties

        public SurveyForm SurveyForm { get; set; }

    }
}
