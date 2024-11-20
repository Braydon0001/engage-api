using Engage.Application.Services.NPrintingBatches.Commands;

namespace Engage.WebApi.Controllers;

public partial class NPrintingBatchController : BaseController
{
    [HttpPost("stage")]
    public async Task<ActionResult<OperationStatus>> Batch(NPrintingBatchStageCommand command, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(command, cancellationToken));
    }

    [HttpPost("process")]
    public async Task<IActionResult> BulkProcess(NPrintingBatchProcessCommand command, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(command, cancellationToken));
    }
}
