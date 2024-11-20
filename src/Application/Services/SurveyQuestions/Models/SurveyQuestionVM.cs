using Engage.Domain.Enums;

namespace Engage.Application.Services.SurveyQuestions.Models;

public class SurveyQuestionVm : IMapFrom<SurveyQuestion>
{
    public int Id { get; set; }
    public int SurveyId { get; set; }
    public OptionDto QuestionTypeId { get; set; }
    public string Question { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsRequired { get; set; }
    public bool IsFalseOptionRequired { get; set; }
    public OptionDto EngageVariantProductId { get; set; }
    public List<OptionDto> QuestionFalseReasonIds { get; set; }
    public List<OptionDto> QuestionOptionIds { get; set; }
    public int? Option1Id { get; set; }
    public int? Option2Id { get; set; }
    public int? Option3Id { get; set; }
    public int? Option4Id { get; set; }
    public int? Option5Id { get; set; }
    public int? Option6Id { get; set; }
    public int? Option7Id { get; set; }
    public int? Option8Id { get; set; }
    public int? Option9Id { get; set; }
    public int? Option10Id { get; set; }
    public string Option1 { get; set; }
    public string Option2 { get; set; }
    public string Option3 { get; set; }
    public string Option4 { get; set; }
    public string Option5 { get; set; }
    public string Option6 { get; set; }
    public string Option7 { get; set; }
    public string Option8 { get; set; }
    public string Option9 { get; set; }
    public string Option10 { get; set; }
    public bool CompleteSurvey1 { get; set; }
    public bool CompleteSurvey2 { get; set; }
    public bool CompleteSurvey3 { get; set; }
    public bool CompleteSurvey4 { get; set; }
    public bool CompleteSurvey5 { get; set; }
    public bool CompleteSurvey6 { get; set; }
    public bool CompleteSurvey7 { get; set; }
    public bool CompleteSurvey8 { get; set; }
    public bool CompleteSurvey9 { get; set; }
    public bool CompleteSurvey10 { get; set; }
    public List<SurveyQuestionRuleDto> VisibleRules { get; set; }
    public List<SurveyQuestionRuleDto> RequiredRules { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyQuestion, SurveyQuestionVm>()
            .ForMember(d => d.Id, opts => opts.MapFrom(s => s.SurveyQuestionId))
            .ForMember(d => d.QuestionTypeId, opts => opts.MapFrom(s => new OptionDto(s.QuestionTypeId, s.QuestionType.Name)))
            .ForMember(d => d.EngageVariantProductId, opts => opts.MapFrom(s =>
                s.EngageVariantProductId.HasValue ?
                    new OptionDto(s.EngageVariantProductId.Value, $"{s.EngageVariantProduct.Code} / {s.EngageVariantProduct.Name}") :
                    null))
            .ForMember(d => d.QuestionFalseReasonIds, opt => opt.MapFrom(s => s.SurveyQuestionFalseReasons.Select(o =>
                                                                            new OptionDto(o.QuestionFalseReasonId, o.QuestionFalseReason.Name))))
            .ForMember(d => d.QuestionOptionIds, opt => opt.MapFrom(s => s.SurveyQuestionOptions.Select(o =>
                                                                            new OptionDto(o.SurveyQuestionOptionId, o.Option))))
            .ForMember(d => d.VisibleRules, opt => opt.MapFrom(q => q.Rules.Where(r => r.RuleType == SurveyQuestionRuleType.visibleRule)))
            .ForMember(d => d.RequiredRules, opt => opt.MapFrom(q => q.Rules.Where(r => r.RuleType == SurveyQuestionRuleType.requiredRule)));

    }

}
