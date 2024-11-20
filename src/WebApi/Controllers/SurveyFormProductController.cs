using Engage.Application.Services.SurveyFormProducts.Commands;

namespace Engage.WebApi.Controllers;

public partial class SurveyFormProductController : BaseController
{

    [HttpPost]
    public async Task<IActionResult> Insert(SurveyFormProductInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SurveyFormProductId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SurveyFormProductUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SurveyFormProductId));
    }

    [HttpPut("batch")]
    public async Task<IActionResult> UpdateBatch(SurveyFormProductBatchUpdateCommand command, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(command, cancellationToken));
    }

}
