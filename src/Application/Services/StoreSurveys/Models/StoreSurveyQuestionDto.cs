using Engage.Application.Services.SurveyQuestionOptions.Models;
using Engage.Application.Services.SurveyQuestions.Models;

namespace Engage.Application.Services.StoreSurveys.Models
{
    public class StoreSurveyQuestionDto : IMapFrom<SurveyQuestion>
    {
        public int SurveyQuestionId { get; set; }
        public int QuestionTypeId { get; set; }
        public int DisplayOrder { get; set; }
        public string Question { get; set; }
        public ICollection<SurveyQuestionOptionDto2> QuestionOptions { get; set; }
        public ICollection<OptionDto> QuestionFalseReasons { get; set; }
        public bool IsFalseOptionRequired { get; set; }
        public ICollection<SurveyQuestionRuleDto> VisibleRules { get; set; }
        public ICollection<SurveyQuestionRuleDto> RequiredRules { get; set; }
        public bool IsRequired { get; set; }
        public OptionDto EngageVariantProductId { get; set; }
        public ICollection<OptionDto> EngageDcProductIds { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<SurveyQuestion, StoreSurveyQuestionDto>()
                .ForMember(d => d.QuestionOptions, opt => opt.MapFrom(s => s.SurveyQuestionOptions))
                .ForMember(d => d.QuestionFalseReasons, opt => opt.MapFrom(s => s.SurveyQuestionFalseReasons
                                                                                    .Select(s => s.QuestionFalseReason)
                                                                                    .Select(s => new OptionDto() { Id = s.Id, Name = s.Name })))
                .ForMember(d => d.IsFalseOptionRequired, opt => opt.MapFrom(s => s.IsFalseOptionRequired))
                .ForMember(d => d.VisibleRules, opt => opt.MapFrom(s => s.Rules.Where(e => e.RuleType == SurveyQuestionRuleType.visibleRule).ToList()))
                .ForMember(d => d.RequiredRules, opt => opt.MapFrom(s => s.Rules.Where(e => e.RuleType == SurveyQuestionRuleType.requiredRule).ToList()))
                .ForMember(d => d.IsRequired, opt => opt.MapFrom(s => s.IsRequired))
                .ForMember(d => d.EngageVariantProductId, opt => opt.MapFrom(s => s.EngageVariantProductId.HasValue ?
                                                                                new OptionDto(s.EngageVariantProductId.Value, $"{s.EngageVariantProduct.Code} / {s.EngageVariantProduct.Name}") :
                                                                                null));
        }
    }
}
