using Engage.Domain.Common;

namespace Engage.Domain.Entities
{
    public class SurveyAnswerOption : BaseAuditableEntity
    {
        public int SurveyAnswerOptionId { get; set; }
        public int SurveyAnswerId { get; set; }
        public int SurveyQuestionOptionId { get; set; }
        
        // Navigation Properties
        public SurveyAnswer SurveyAnswer { get; set; }                                
        public SurveyQuestionOption SurveyQuestionOption { get; set; }                                
    }
}
