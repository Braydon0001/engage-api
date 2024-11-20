using Engage.Application.Services.Roles.Commands;
using Engage.Application.Services.Roles.Queries;

namespace Engage.WebApi.Controllers;

public partial class RoleController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<RoleDto>>> List([FromQuery] RoleListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<RoleDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<RoleOption>>> Options([FromQuery] RoleOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RoleVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new RoleVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpGet("{id}/permissions")]
    public async Task<ActionResult<RolePermissionVm>> RolePermissionVm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new RolePermissionVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(RoleInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.RoleId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(RoleUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.RoleId));
    }

}
