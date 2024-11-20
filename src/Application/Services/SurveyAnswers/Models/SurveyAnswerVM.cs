using Engage.Application.Services.SurveyQuestions.Models;

namespace Engage.Application.Services.SurveyAnswers.Models;

public class SurveyAnswerVM
{
    public StoreSurveyAnswerDto SurveyAnswer { get; set; }

    public SurveyQuestionVm SurveyQuestion { get; set; }
}
