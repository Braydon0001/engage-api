namespace Engage.Application.Services.SurveyFormQuestions.Queries;

public class SurveyFormQuestionDto : IMapFrom<SurveyFormQuestion>
{
    public int Id { get; init; }
    public string QuestionText { get; init; }
    public int? DisplayOrder { get; init; }
    public bool IsRequired { get; init; }
    public string Notes { get; init; }
    public List<JsonRule> Rules { get; init; }
    public List<JsonFile> Files { get; init; }
    public List<JsonSetting> Metadata { get; init; }
    public int SurveyFormQuestionGroupId { get; init; }
    public string SurveyFormQuestionGroupName { get; init; }
    public OptionDto SurveyFormQuestionGroup { get; init; }
    public int SurveyFormQuestionTypeId { get; init; }
    public string SurveyFormQuestionTypeName { get; init; }
    public bool IsReasonRequired { get; init; }
    public DateTime? MinDateTime { get; init; }
    public DateTime? MaxDateTime { get; init; }
    public bool Disabled { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormQuestion, SurveyFormQuestionDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormQuestionId))
               .ForMember(d => d.SurveyFormQuestionGroup, opt => opt.MapFrom(s => new OptionDto() { Id = s.SurveyFormQuestionGroupId, Name = s.SurveyFormQuestionGroup.Name, Disabled = s.SurveyFormQuestionGroup.Disabled }));
    }
}
