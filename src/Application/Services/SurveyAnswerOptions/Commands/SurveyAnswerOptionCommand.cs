namespace Engage.Application.Services.SurveyAnswerOptions.Commands;

public class SurveyAnswerOptionCommand : IMapTo<SurveyAnswerOption>
{
    public int SurveyAnswerId { get; set; }
    public int SurveyQuestionOptionId { get; set; }

    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<SurveyAnswerOptionCommand, SurveyAnswerOption>();
    }
}
