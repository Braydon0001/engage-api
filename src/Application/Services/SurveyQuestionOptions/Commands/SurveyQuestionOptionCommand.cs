namespace Engage.Application.Services.SurveyQuestionOptions.Commands;

public class SurveyQuestionOptionCommand : IMapTo<SurveyQuestionOption>
{
    public string Option { get; set; }
    public bool CompleteSurvey { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyQuestionOptionCommand, SurveyQuestionOption>();
    }
}
