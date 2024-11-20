using Engage.Application.Services.CreditorNotificationStatusUsers.Commands;
using Engage.Application.Services.CreditorNotificationStatusUsers.Models;
using Engage.Application.Services.CreditorNotificationStatusUsers.Queries;

namespace Engage.WebApi.Controllers;

public class CreditorNotificationStatusUserController : BaseController
{
    [HttpGet("creditorstatusid/{creditorstatusid}/engageregionid/{engageregionid}")]
    public async Task<ActionResult<PaginatedListResult<CreditorNotificationStatusUserDto>>> Get([FromRoute] CreditorNotificationStatusUsersQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    public async Task<ActionResult<ListResult<CreditorNotificationStatusUserDto>>> GetAll([FromQuery] CreditorNotificationStatusUsersQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CreditorNotificationStatusUserVm>> GetVm([FromRoute] CreditorNotificationStatusUserVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost("batch")]
    public async Task<IActionResult> BatchCreateCreditorNotificationStatusUsers(BatchCreateCreditorNotificationStatusUsersCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(DeleteCreditorNotificationStatusUserCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }
}
