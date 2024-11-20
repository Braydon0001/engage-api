using Engage.Application.Services.SurveyFormQuestionProducts.Commands;

namespace Engage.WebApi.Controllers;

public partial class SurveyFormQuestionProductController : BaseController
{

    [HttpPost]
    public async Task<IActionResult> Insert(SurveyFormQuestionProductInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SurveyFormQuestionProductId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SurveyFormQuestionProductUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SurveyFormQuestionProductId));
    }

}
