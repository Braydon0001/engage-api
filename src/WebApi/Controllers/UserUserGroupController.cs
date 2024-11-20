using Engage.Application.Services.UserUserGroups.Models;
using Engage.Application.Services.UserUserGroups.Queries;
using Engage.Application.Services.UserUserGroups.Commands;

namespace Engage.WebApi.Controllers;

public class UserUserGroupController : BaseController
{
    [HttpGet()]
    public async Task<ActionResult<ListResult<UserUserGroupDto>>> GetAll([FromQuery] UserUserGroupQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("sync")]
    public async Task<ActionResult<ListResult<UserUserGroupDto>>> Sync([FromQuery] UserUserGroupSyncQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserUserGroupCreateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("batch")]
    public async Task<IActionResult> BatchCreate(UserUserGroupBatchCreateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UserUserGroupUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{userId}/{userGroupId}")]
    public async Task<IActionResult> Deleted([FromRoute] int userId, int userGroupId)
    {
        return Ok(await Mediator.Send(new UserUserGroupDeleteCommand
        {
            UserId = userId,
            UserGroupId = userGroupId
        }));
    }
}
