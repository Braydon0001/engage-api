using Engage.Application.Services.NotificationEmployeeReads.Commands;
using Engage.Application.Services.NotificationEmployeeReads.Models;
using Engage.Application.Services.NotificationEmployeeReads.Queries;

namespace Engage.WebApi.Controllers;

public class NotificationEmployeeReadController : BaseController
{
    [HttpGet("employeenotificationreads/{employeeId}")]
    public async Task<ActionResult<ListResult<EmployeeNotificationReadsDto>>> GetEmployeeNotificationReads([FromRoute] GetEmployeeNotificationReadsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPut("read")]
    public async Task<IActionResult> UpdateRead(UpdateNotificationEmployeeReadCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("batchread")]
    public async Task<IActionResult> UpdateBatchRead(UpdateBatchNotificationEmployeeReadCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
