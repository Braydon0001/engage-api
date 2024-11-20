namespace Engage.Domain.Entities
{
    public class SurveyFormAnswerHistory : BaseAuditableEntity
    {
        public SurveyFormAnswerHistory()
        {
            SurveyFormAnswerOptionHistories = new HashSet<SurveyFormAnswerOptionHistory>();
        }

        public int SurveyFormAnswerHistoryId { get; set; }
        public int SurveyFormAnswerId { get; set; }
        public int? SurveyFormReasonId { get; set; }
        public string AnswerText { get; set; }
        public List<JsonFile> Files { get; set; }

        // Navigation Properties
        public SurveyFormAnswer SurveyFormAnswer { get; set; }
        public SurveyFormReason SurveyFormReason { get; set; }
        public ICollection<SurveyFormAnswerOptionHistory> SurveyFormAnswerOptionHistories { get; set; }
    }
}
