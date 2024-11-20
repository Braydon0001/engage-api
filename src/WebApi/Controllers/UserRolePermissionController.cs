using Engage.Application.Services.UserRolePermissions.Commands;
using Engage.Application.Services.UserRolePermissions.Queries;

namespace Engage.WebApi.Controllers;

public partial class UserRolePermissionController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<UserRolePermissionDto>>> List([FromQuery] UserRolePermissionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<UserRolePermissionDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<UserRolePermissionOption>>> Options([FromQuery] UserRolePermissionOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserRolePermissionVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new UserRolePermissionVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(UserRolePermissionInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.UserRolePermissionId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UserRolePermissionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.UserRolePermissionId));
    }

}
