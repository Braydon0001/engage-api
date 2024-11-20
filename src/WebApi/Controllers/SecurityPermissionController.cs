using Engage.Application.Services.SecurityPermissions.Commands;
using Engage.Application.Services.SecurityPermissions.Queries;

namespace Engage.WebApi.Controllers;

public partial class SecurityPermissionController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SecurityPermissionDto>>> List([FromQuery] SecurityPermissionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SecurityPermissionDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<SecurityPermissionOption>>> Options([FromQuery] SecurityPermissionOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SecurityPermissionVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SecurityPermissionVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SecurityPermissionInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SecurityPermissionId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SecurityPermissionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SecurityPermissionId));
    }

}
