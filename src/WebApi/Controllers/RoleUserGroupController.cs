using Engage.Application.Services.RoleUserGroups.Commands;
using Engage.Application.Services.RoleUserGroups.Queries;

namespace Engage.WebApi.Controllers;

public partial class RoleUserGroupController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<RoleUserGroupDto>>> List([FromQuery] RoleUserGroupListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<RoleUserGroupDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<RoleUserGroupOption>>> Options([FromQuery] RoleUserGroupOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RoleUserGroupVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new RoleUserGroupVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(RoleUserGroupInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.RoleUserGroupId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(RoleUserGroupUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.RoleUserGroupId));
    }

}
