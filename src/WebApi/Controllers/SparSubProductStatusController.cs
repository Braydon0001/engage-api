using Engage.Application.Services.SparSubProductStatuses.Commands;
using Engage.Application.Services.SparSubProductStatuses.Queries;

namespace Engage.WebApi.Controllers;

public partial class SparSubProductStatusController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SparSubProductStatusDto>>> List([FromQuery] SparSubProductStatusListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SparSubProductStatusDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<SparSubProductStatusOption>>> Options([FromQuery] SparSubProductStatusOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SparSubProductStatusVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SparSubProductStatusVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SparSubProductStatusInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SparSubProductStatusId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SparSubProductStatusUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SparSubProductStatusId));
    }

}
