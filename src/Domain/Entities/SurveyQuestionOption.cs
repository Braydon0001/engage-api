using System.Collections.Generic;
using Engage.Domain.Common;

namespace Engage.Domain.Entities
{
    public class SurveyQuestionOption : BaseAuditableEntity    
    {
        public SurveyQuestionOption()
        {
            SurveyAnswerOptions = new HashSet<SurveyAnswerOption>();
        }

        public int SurveyQuestionOptionId { get; set; }
        public int SurveyQuestionId { get; set; }
        public string Option { get; set; }
        public int DisplayOrder { get; set; }
        public bool CompleteSurvey { get; set; }

        // Navigation Properties
        public SurveyQuestion SurveyQuestion { get; set; }
        public ICollection<SurveyAnswerOption> SurveyAnswerOptions { get; set; }
    }
}
