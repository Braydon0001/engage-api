namespace Engage.Domain.Entities
{
    public class SurveyQuestionRule : BaseAuditableEntity
    {
        public int SurveyQuestionRuleId { get; set; }
        public int QuestionId { get; set; }
        public int TargetQuestionId { get; set; }
        public string Operation { get; set; }
        public int RuleIndex { get; set; }
        public string RuleText { get; set; }
        public string Value { get; set; }
        public SurveyQuestionRuleValueType ValueType { get; set; }
        public SurveyQuestionRuleType RuleType { get; set; }

        // Navigation Properties
        public SurveyQuestion Question { get; set; }
        public SurveyQuestion TargetQuestion { get; set; }
    }
}
