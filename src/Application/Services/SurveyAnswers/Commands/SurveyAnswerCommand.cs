namespace Engage.Application.Services.SurveyAnswers.Commands;

public class SurveyAnswerCommand : IMapTo<SurveyAnswer>
{

    public int? QuestionFalseReasonId { get; set; }
    public string Answer { get; set; }

    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<SurveyAnswerCommand, SurveyAnswer>();
    }
}
