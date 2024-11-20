namespace Engage.Domain.Entities
{
    public class SurveyFormAnswerOptionHistory : BaseAuditableEntity
    {
        public int SurveyFormAnswerOptionHistoryId { get; set; }
        public int SurveyFormAnswerHistoryId { get; set; }
        public int? SurveyFormOptionId { get; set; }

        // Navigation Properties
        public SurveyFormAnswerHistory SurveyFormAnswerHistory { get; set; }
        public SurveyFormOption SurveyFormOption { get; set; }
    }
}
