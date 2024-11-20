using Engage.Application.Services.UserRoles.Commands;
using Engage.Application.Services.UserRoles.Queries;

namespace Engage.WebApi.Controllers;

public partial class UserRoleController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<UserRoleDto>>> List([FromQuery] UserRoleListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<UserRoleDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<UserRoleOption>>> Options([FromQuery] UserRoleOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserRoleVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new UserRoleVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(UserRoleInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.UserRoleId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UserRoleUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.UserRoleId));
    }

}
