// auto-generated
using Engage.Application.Services.NotificationStores.Commands;

namespace Engage.WebApi.Controllers;

public partial class NotificationStoreController : BaseController
{
    [HttpPost("bulkinsert")]
    public async Task<IActionResult> BulkInsert(NotificationStoreBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var insertedIds = await Mediator.Send(command, cancellationToken);

        return insertedIds == null ? NotFound() : Ok(new OperationStatus(insertedIds.Count, command.NotificationId));
    }

    [HttpPut("bulkdelete")]
    public async Task<IActionResult> BulkDelete(NotificationStoreBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var deleteCount = await Mediator.Send(command, cancellationToken);

        return !deleteCount.HasValue ? NotFound() : Ok(new OperationStatus(deleteCount.Value, command.NotificationId));
    }


}