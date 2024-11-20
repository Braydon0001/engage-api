using Engage.Application.Services.Shared.AssignCommands;

namespace Engage.Application.Services.Surveys.Commands;

public static class SurveyAssigns
{
    public static async Task BatchAssign(IMediator mediator, SurveyCommand command, Survey entity)
    {
        if (command.EngageRegions != null)
        {
            await mediator.Send(new BatchAssignCommand(
                AssignDesc.REGION_SURVEY, entity.SurveyId, command.EngageRegions));
        }

        if (command.Stores != null)
        {
            await mediator.Send(new BatchAssignCommand(
                AssignDesc.STORE_SURVEY, entity.SurveyId, command.Stores));
        }

    }
}
