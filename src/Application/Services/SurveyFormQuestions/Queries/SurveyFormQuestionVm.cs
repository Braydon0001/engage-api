
using Engage.Application.Services.SurveyFormQuestionGroups.Queries;
using Engage.Application.Services.SurveyFormQuestions.Commands;
using Engage.Application.Services.SurveyFormQuestionTypes.Queries;

namespace Engage.Application.Services.SurveyFormQuestions.Queries;

public class SurveyFormQuestionVm : IMapFrom<SurveyFormQuestion>
{
    public int Id { get; init; }
    public string QuestionText { get; init; }
    public int? DisplayOrder { get; init; }
    public bool IsRequired { get; init; }
    public string Notes { get; init; }
    public List<JsonRule> Rules { get; init; }
    public List<JsonFile> Files { get; init; }
    public List<JsonFile> SurveyFormQuestionFiles { get; init; }
    public List<JsonSetting> Metadata { get; init; }
    public SurveyFormQuestionGroupOption SurveyFormQuestionGroupId { get; init; }
    public SurveyFormQuestionTypeOption SurveyFormQuestionTypeId { get; init; }
    public bool IsReasonRequired { get; init; }
    public bool? IsFalseReasonRequired { get; set; }
    public DateTime? MinDateTime { get; init; }
    public DateTime? MaxDateTime { get; init; }
    public List<OptionDto> EngageVariantProductIds { get; set; }
    public List<ReasonOption> AnswerReasons { get; set; }
    public List<ReasonOption> AnswerOptions { get; set; }
    public List<JsonLink> Links { get; set; }
    public List<ValueVm> Values { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormQuestion, SurveyFormQuestionVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormQuestionId))
               .ForMember(d => d.SurveyFormQuestionGroupId, opt => opt.MapFrom(s => s.SurveyFormQuestionGroup))
               .ForMember(d => d.SurveyFormQuestionTypeId, opt => opt.MapFrom(s => s.SurveyFormQuestionType))
               .ForMember(d => d.IsFalseReasonRequired, opt => opt.MapFrom(s => s.Metadata.Where(e => e.Name == "IsFalseReasonRequired").Select<JsonSetting, bool?>(e => e.Value == null ? null : e.Value == "True").FirstOrDefault()))
               .ForMember(d => d.EngageVariantProductIds, opt => opt.MapFrom(s => new List<OptionDto>(s.SurveyFormQuestionProducts.Select(p => new OptionDto() { Id = p.EngageVariantProductId, Name = p.EngageVariantProduct.Code + " / " + p.EngageVariantProduct.Name }).ToList())))
               .ForMember(d => d.AnswerOptions, opt => opt.MapFrom(s => new List<ReasonOption>(s.SurveyFormQuestionOptions.Select(o => new ReasonOption() { Text = o.SurveyFormOption.Name, Id = o.SurveyFormOption.SurveyFormOptionId, CompleteSurvey = o.SurveyFormOption.CompleteSurvey }).ToList())))
               .ForMember(d => d.AnswerReasons, opt => opt.MapFrom(s => new List<ReasonOption>(s.SurveyFormQuestionReasons.Select(r => new ReasonOption() { Text = r.SurveyFormReason.Name, Id = r.SurveyFormReason.SurveyFormReasonId, CompleteSurvey = r.SurveyFormReason.CompleteSurvey }).ToList())))
               .ForMember(d => d.SurveyFormQuestionFiles, opt => opt.MapFrom(s => s.Files))
               .ForMember(d => d.Values, opt => opt.MapFrom(s => s.Metadata.Where(e => e.Name == "Values").Select(v => JsonConvert.DeserializeObject<List<ValueVm>>(v.Value)).FirstOrDefault()));
    }
}
