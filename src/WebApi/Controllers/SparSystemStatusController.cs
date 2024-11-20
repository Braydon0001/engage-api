using Engage.Application.Services.SparSystemStatuses.Commands;
using Engage.Application.Services.SparSystemStatuses.Queries;

namespace Engage.WebApi.Controllers;

public partial class SparSystemStatusController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SparSystemStatusDto>>> List([FromQuery] SparSystemStatusListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SparSystemStatusDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<SparSystemStatusOption>>> Options([FromQuery] SparSystemStatusOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SparSystemStatusVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SparSystemStatusVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SparSystemStatusInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SparSystemStatusId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SparSystemStatusUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SparSystemStatusId));
    }

}
