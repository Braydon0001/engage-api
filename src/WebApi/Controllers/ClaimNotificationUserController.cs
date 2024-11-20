using Engage.Application.Services.ClaimNotificationUsers.Commands;
using Engage.Application.Services.ClaimNotificationUsers.Models;
using Engage.Application.Services.ClaimNotificationUsers.Queries;

namespace Engage.WebApi.Controllers;

public class ClaimNotificationUserController : BaseController
{
    [HttpGet("claimstatusid/{claimstatusid}/engageregionid/{engageregionid}")]
    public async Task<ActionResult<PaginatedListResult<ClaimNotificationUserDto>>> Get([FromRoute] ClaimNotificationUsersQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    public async Task<ActionResult<ListResult<ClaimNotificationUserDto>>> GetAll([FromQuery] ClaimNotificationUsersQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClaimNotificationUserVm>> GetVm([FromRoute] ClaimNotificationUserVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateClaimNotificationUserCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateClaimNotificationUserCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("batch")]
    public async Task<IActionResult> BatchCreateClaimNotificationUsers(BatchCreateClaimNotificationUsersCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await Mediator.Send(new DeleteClaimNotificationUserCommand
        {
            Id = id,
        }));
    }
}
