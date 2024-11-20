using Engage.Application.Services.SurveyQuestionRules.Commands;

namespace Engage.Application.Services.SurveyQuestions.Commands;

public class SurveyQuestionCommand : IMapTo<SurveyQuestion>
{
    public int QuestionTypeId { get; set; }
    public string Question { get; set; }
    public List<int> QuestionFalseReasonIds { get; set; }
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

    public bool IsRequired { get; set; }
    public bool IsFalseOptionRequired { get; set; }
    public int? EngageVariantProductId { get; set; }
    public List<QuestionSurveyRuleUpsertCommand> VisibleRules { get; set; }
    public List<QuestionSurveyRuleUpsertCommand> RequiredRules { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyQuestionCommand, SurveyQuestion>();
    }
}
