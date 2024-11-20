using Engage.Application.Services.SparProductStatuses.Commands;
using Engage.Application.Services.SparProductStatuses.Queries;

namespace Engage.WebApi.Controllers;

public partial class SparProductStatusController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SparProductStatusDto>>> List([FromQuery] SparProductStatusListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SparProductStatusDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<SparProductStatusOption>>> Options([FromQuery] SparProductStatusOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SparProductStatusVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SparProductStatusVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SparProductStatusInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SparProductStatusId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SparProductStatusUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SparProductStatusId));
    }

}
