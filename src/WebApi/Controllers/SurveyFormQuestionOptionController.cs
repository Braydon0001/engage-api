using Engage.Application.Services.SurveyFormQuestionOptions.Commands;

namespace Engage.WebApi.Controllers;

public partial class SurveyFormQuestionOptionController : BaseController
{

    [HttpPost]
    public async Task<IActionResult> Insert(SurveyFormQuestionOptionInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SurveyFormQuestionOptionId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SurveyFormQuestionOptionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SurveyFormQuestionOptionId));
    }

}
