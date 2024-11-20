using Engage.Application.Services.ClaimStatusUsers.Commands;
using Engage.Application.Services.ClaimStatusUsers.Models;
using Engage.Application.Services.ClaimStatusUsers.Queries;

namespace Engage.WebApi.Controllers;

public class ClaimStatusUserController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ClaimStatusUserDto>>> GetAll([FromQuery] GetClaimStatusUsersQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClaimStatusUserVm>> GetVm([FromRoute] GetClaimStatusUserVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateClaimStatusUserCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
