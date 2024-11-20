// auto-generated
using Engage.Application.Services.NotificationEmployeeJobTitles.Commands;

namespace Engage.WebApi.Controllers;

public partial class NotificationEmployeeJobTitleController : BaseController
{
    [HttpPost("bulkinsert")]
    public async Task<IActionResult> BulkInsert(NotificationEmployeeJobTitleBulkInsertCommand command, CancellationToken cancellationToken)
    {
      var insertedIds = await Mediator.Send(command, cancellationToken);

      return insertedIds == null ? NotFound() : Ok(new OperationStatus(insertedIds.Count, command.NotificationId));
    }

    [HttpPut("bulkdelete")]
    public async Task<IActionResult> BulkDelete(NotificationEmployeeJobTitleBulkDeleteCommand command, CancellationToken cancellationToken)
    {
      var deleteCount = await Mediator.Send(command, cancellationToken);

      return !deleteCount.HasValue ?  NotFound() : Ok(new OperationStatus(deleteCount.Value, command.NotificationId));
    }


}