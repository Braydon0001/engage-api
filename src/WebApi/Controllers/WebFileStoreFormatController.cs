// auto-generated
using Engage.Application.Services.WebFileStoreFormats.Commands;

namespace Engage.WebApi.Controllers;

public partial class WebFileStoreFormatController : BaseController
{
    [HttpPost("bulkinsert")]
    public async Task<IActionResult> BulkInsert(WebFileStoreFormatBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var insertedIds = await Mediator.Send(command, cancellationToken);

        return insertedIds == null ? NotFound() : Ok(new OperationStatus(insertedIds.Count, command.WebFileId));
    }

    [HttpPut("bulkdelete")]
    public async Task<IActionResult> BulkDelete(WebFileStoreFormatBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var deleteCount = await Mediator.Send(command, cancellationToken);

        return !deleteCount.HasValue ? NotFound() : Ok(new OperationStatus(deleteCount.Value, command.WebFileId));
    }


}