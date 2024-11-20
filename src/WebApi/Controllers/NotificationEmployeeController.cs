using Engage.Application.Services.NotificationEmployees.Commands;
using Engage.Application.Services.NotificationEmployees.Queries;
using Engage.Application.Services.Notifications.Models;

namespace Engage.WebApi.Controllers;

public partial class NotificationEmployeeController : BaseController
{
    [HttpGet()]
    public async Task<ActionResult<ListResult<NotificationDto>>> Targeted([FromQuery] int employeeId, [FromQuery] int channelId, [FromQuery] DateTime date)
    {
        if (employeeId <= 0)
        {
            return BadRequest(BadIdText);
        }

        if (channelId <= 0)
        {
            return BadRequest(BadIdText);
        }

        var result = await Mediator.Send(new NotificationEmployeeTargetedQuery(employeeId, channelId, date));

        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost("bulkinsert")]
    public async Task<IActionResult> BulkInsert(NotificationEmployeeBulkInsertCommand command, CancellationToken cancellationToken)
    {
        var insertedIds = await Mediator.Send(command, cancellationToken);

        return insertedIds == null ? NotFound() : Ok(new OperationStatus(insertedIds.Count, command.NotificationId));
    }

    [HttpPut("bulkdelete")]
    public async Task<IActionResult> BulkDelete(NotificationEmployeeBulkDeleteCommand command, CancellationToken cancellationToken)
    {
        var deleteCount = await Mediator.Send(command, cancellationToken);

        return !deleteCount.HasValue ? NotFound() : Ok(new OperationStatus(deleteCount.Value, command.NotificationId));
    }
}