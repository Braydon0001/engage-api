using Engage.Application.Services.WebFileStores.Commands;
using Engage.Application.Services.WebFileStores.Queries;

namespace Engage.WebApi.Controllers;

public partial class WebFileStoreController : BaseController
{
    [HttpGet()]
    public async Task<ActionResult<WebFileStoreTargeted>> Targeted([FromQuery] int storeId, [FromQuery] DateTime date)
    {
        if (storeId <= 0)
        {
            return BadRequest(BadIdText);
        }

        var result = await Mediator.Send(new WebFileStoreTargetedQuery(storeId, date));

        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost("bulkinsert")]
    public async Task<IActionResult> BulkInsert(WebFileStoreBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var insertedIds = await Mediator.Send(command, cancellationToken);

        return insertedIds == null ? NotFound() : Ok(new OperationStatus(insertedIds.Count, command.WebFileId));
    }

    [HttpPut("bulkdelete")]
    public async Task<IActionResult> BulkDelete(WebFileStoreBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var deleteCount = await Mediator.Send(command, cancellationToken);

        return !deleteCount.HasValue ? NotFound() : Ok(new OperationStatus(deleteCount.Value, command.WebFileId));
    }


}