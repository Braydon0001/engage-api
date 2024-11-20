// auto-generated
using Engage.Application.Services.WebFileEngageRegions.Commands;

namespace Engage.WebApi.Controllers;

public partial class WebFileEngageRegionController : BaseController
{
    [HttpPost("bulkinsert")]
    public async Task<IActionResult> BulkInsert(WebFileEngageRegionBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var insertedIds = await Mediator.Send(command, cancellationToken);

        return insertedIds == null ? NotFound() : Ok(new OperationStatus(insertedIds.Count, command.WebFileId));
    }

    [HttpPut("bulkdelete")]
    public async Task<IActionResult> BulkDelete(WebFileEngageRegionBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var deleteCount = await Mediator.Send(command, cancellationToken);

        return !deleteCount.HasValue ? NotFound() : Ok(new OperationStatus(deleteCount.Value, command.WebFileId));
    }


}