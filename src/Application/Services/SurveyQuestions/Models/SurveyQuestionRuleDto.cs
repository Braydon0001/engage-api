

namespace Engage.Application.Services.SurveyQuestions.Models
{
    public class SurveyQuestionRuleDto: IMapFrom<SurveyQuestionRule>
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

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SurveyQuestionRule, SurveyQuestionRuleDto>()
                //.ForMember(d => d.Id, opt => opt.MapFrom(d => d.SurveyQuestionRuleId))
                .ForMember(d => d.QuestionId, opt => opt.MapFrom(d => d.QuestionId))
                .ForMember(d => d.TargetQuestionId, opt => opt.MapFrom(d => d.TargetQuestionId))
                .ForMember(d => d.Operation, opt => opt.MapFrom(d => d.Operation))
                .ForMember(d => d.RuleIndex, opt => opt.MapFrom(d => d.RuleIndex))
                .ForMember(d => d.RuleText, opt => opt.MapFrom(d => d.RuleText))
                .ForMember(d => d.Value, opt => opt.MapFrom(d => d.Value))
                .ForMember(d => d.ValueType, opt => opt.MapFrom(d => d.ValueType))
                .ForMember(d => d.RuleType, opt => opt.MapFrom(d => d.RuleType));
        }
    }
}
