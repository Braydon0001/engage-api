using Engage.Application.Services.SecurityRoles.Commands;
using Engage.Application.Services.SecurityRoles.Queries;

namespace Engage.WebApi.Controllers;

public partial class SecurityRoleController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SecurityRoleDto>>> List([FromQuery] SecurityRoleListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SecurityRoleDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<SecurityRoleOption>>> Options([FromQuery] SecurityRoleOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("page/options")]
    public async Task<ActionResult<List<SecurityRoleOption>>> PaginatedOptions([FromQuery] SecurityRolePageinatedOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);
        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SecurityRoleVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SecurityRoleVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SecurityRoleInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SecurityRoleId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SecurityRoleUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SecurityRoleId));
    }

}
