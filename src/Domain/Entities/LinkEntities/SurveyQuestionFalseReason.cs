namespace Engage.Domain.Entities.LinkEntities
{
    public class SurveyQuestionFalseReason
    {
        public int SurveyQuestionId { get; set; }
        public int QuestionFalseReasonId { get; set; }

        // Navigation Properties
        public SurveyQuestion SurveyQuestion { get; set; }
        public QuestionFalseReason QuestionFalseReason { get; set; }
    }
}
