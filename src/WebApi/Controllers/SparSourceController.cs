using Engage.Application.Services.SparSources.Commands;
using Engage.Application.Services.SparSources.Queries;

namespace Engage.WebApi.Controllers;

public partial class SparSourceController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SparSourceDto>>> List([FromQuery] SparSourceListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SparSourceDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<SparSourceOption>>> Options([FromQuery] SparSourceOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SparSourceVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SparSourceVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SparSourceInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SparSourceId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SparSourceUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SparSourceId));
    }

}
