using Engage.Application.Services.AppUsers.Commands;
using Engage.Application.Services.Mobile.User.Models;
using Engage.Application.Services.Mobile.User.Queries;
using Engage.Application.Services.Users.Queries;

namespace Engage.WebApi.Controllers.Mobile;

public class UserController : BaseMobileController
{

    [HttpPost("GetEmployeeId")]
    public async Task<OperationStatus> GetEmployeeId(GetUserIdCommand command)
    {
        return await Mediator.Send(command);
    }

    [AllowAnonymous]
    [HttpGet("employeeid")]
    public async Task<ActionResult<int>> GetEmployeeId([FromRoute] UserEmployeeIdQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("info")]
    public async Task<ActionResult<UserInfo>> GetUserInfo(CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(new UserInfoQuery(), cancellationToken);
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<UserOption>>> PaginatedOptionList([FromQuery] UserMobileOptionQuery query)
    {
        return Ok(await Mediator.Send(query));
    }


    [HttpPost("CheckMobileVersion")]
    public async Task<OperationStatus> CheckMobileVersion(CheckMobileVersionCommand command)
    {
        return await Mediator.Send(command);
    }


    [HttpGet("GetUserGroups")]
    public async Task<ActionResult<ListResult<UserGroupDto>>> GetUserGroups([FromQuery] GetUserGroupsQuery query)
    {

        return Ok(await Mediator.Send(query));
    }

}
