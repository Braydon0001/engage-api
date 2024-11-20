namespace Engage.Domain.Entities
{
    public class SurveyAnswer : BaseAuditableEntity
    {
        public SurveyAnswer()
        {
            SurveyAnswerOptions = new HashSet<SurveyAnswerOption>();
            SurveyAnswerPhotos = new HashSet<SurveyAnswerPhoto>();
        }

        public int SurveyAnswerId { get; set; }
        public int SurveyInstanceId { get; set; }
        public int SurveyQuestionId { get; set; }
        public int? QuestionFalseReasonId { get; set; }
        public string Answer { get; set; }
        public List<JsonFile> Files { get; set; }

        // Navigation Properties
        public SurveyInstance SurveyInstance { get; set; }
        public SurveyQuestion SurveyQuestion { get; set; }
        public QuestionFalseReason QuestionFalseReason { get; set; }
        public ICollection<SurveyAnswerOption> SurveyAnswerOptions { get; set; }
        public ICollection<SurveyAnswerPhoto> SurveyAnswerPhotos { get; set; }
    }
}
