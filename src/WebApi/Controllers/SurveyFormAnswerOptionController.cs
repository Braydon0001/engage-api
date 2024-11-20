using Engage.Application.Services.SurveyFormAnswerOptions.Commands;

namespace Engage.WebApi.Controllers;

public partial class SurveyFormAnswerOptionController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Insert(SurveyFormAnswerOptionInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SurveyFormAnswerOptionId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SurveyFormAnswerOptionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SurveyFormAnswerOptionId));
    }

}
