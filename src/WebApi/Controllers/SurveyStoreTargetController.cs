// auto-generated

using Engage.Application.Services.SurveyStores.Commands;

namespace Engage.WebApi.Controllers;

public partial class SurveyStoreTargetController : BaseController
{
    [HttpPost("bulkinsert")]
    public async Task<IActionResult> BulkInsert(SurveyStoreBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var insertedIds = await Mediator.Send(command, cancellationToken);

        return insertedIds == null ? NotFound() : Ok(new OperationStatus(insertedIds.Count, command.SurveyId));
    }

    [HttpPut("bulkdelete")]
    public async Task<IActionResult> BulkDelete(SurveyStoreBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var deleteCount = await Mediator.Send(command, cancellationToken);

        return !deleteCount.HasValue ? NotFound() : Ok(new OperationStatus(deleteCount.Value, command.SurveyId));
    }
}