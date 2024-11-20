using Engage.Application.Services.SurveyQuestionOptions.Commands;
using Engage.Application.Services.SurveyQuestions.Models;

namespace Engage.Application.Services.SurveyQuestions.Commands;

public static class UpsertOption
{
    public static async Task<OperationStatus> Create(IMediator mediator, int surveyQuestionId, OptionValue option)
    {
        return await mediator.Send(new CreateSurveyQuestionOptionCommand()
        {
            SurveyQuestionId = surveyQuestionId,
            Option = option.Value,
            CompleteSurvey = option.CompleteSurvey
        });
    }

    public static async Task<OperationStatus> Update(IMediator mediator, int optionId, OptionValue option)
    {
        return await mediator.Send(new UpdateSurveyQuestionOptionCommand()
        {
            Id = optionId,
            Option = option.Value,
            CompleteSurvey = option.CompleteSurvey
        });
    }
}
