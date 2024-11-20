using Engage.Application.Services.UserPermissions.Commands;
using Engage.Application.Services.UserPermissions.Queries;

namespace Engage.WebApi.Controllers;

public partial class UserPermissionController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<UserPermissionDto>>> List([FromQuery] UserPermissionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<UserPermissionDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<UserPermissionOption>>> Options([FromQuery] UserPermissionOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserPermissionVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new UserPermissionVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(UserPermissionInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.UserPermissionId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UserPermissionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.UserPermissionId));
    }

}
