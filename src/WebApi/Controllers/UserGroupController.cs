using Engage.Application.Services.UserGroups.Commands;
using Engage.Application.Services.UserGroups.Models;
using Engage.Application.Services.UserGroups.Queries;

namespace Engage.WebApi.Controllers;

public class UserGroupController : BaseController
{
    [HttpGet()]
    public async Task<ActionResult<ListResult<UserGroupDto>>> GetAll([FromQuery] UserGroupQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("sync")]
    public async Task<ActionResult<ListResult<UserGroupDto>>> Sync([FromQuery] UserGroupSyncQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{usergroupId}")]
    public async Task<ActionResult<UserGroupDto>> GetById([FromQuery] UserGroupVmQuery query, [FromRoute] int UserGroupId)
    {
        query.Id = UserGroupId;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("getVendorId/{usergroupId}")]
    public async Task<ActionResult<VendorIdVm>> GetVendorId([FromQuery] UserGroupVendorIdQuery query, [FromRoute] int UserGroupId)
    {
        query.Id = UserGroupId;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("options")]
    public async Task<ActionResult<List<OptionDto>>> Options([FromQuery] UserGroupOptionQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("permissions/{userId}")]
    public async Task<ActionResult<List<UserGroupPermission>>> Permissions([FromQuery] UserGroupPermissionsQuery query, [FromRoute] int UserId)
    {
        query.UserId = UserId;
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserGroupCreateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UserGroupUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCommand(UserGroupDeleteCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await Mediator.Send(new UserGroupDeleteCommand
        {
            Id = id,
        }));
    }
}
