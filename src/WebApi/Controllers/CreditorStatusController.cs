using Engage.Application.Services.CreditorStatuses.Commands;
using Engage.Application.Services.CreditorStatuses.Queries;

namespace Engage.WebApi.Controllers;

public partial class CreditorStatusController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<CreditorStatusDto>>> List([FromQuery] CreditorStatusListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<CreditorStatusDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<CreditorStatusOption>>> Options([FromQuery] CreditorStatusOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CreditorStatusVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new CreditorStatusVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(CreditorStatusInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.CreditorStatusId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(CreditorStatusUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.CreditorStatusId));
    }

}
