using Engage.Application.Services.SurveyFormQuestionReasons.Commands;

namespace Engage.WebApi.Controllers;

public partial class SurveyFormQuestionReasonController : BaseController
{

    [HttpPost]
    public async Task<IActionResult> Insert(SurveyFormQuestionReasonInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SurveyFormQuestionReasonId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SurveyFormQuestionReasonUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SurveyFormQuestionReasonId));
    }

}
