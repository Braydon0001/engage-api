using Engage.Application.Services.SparUnitTypes.Commands;
using Engage.Application.Services.SparUnitTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class SparUnitTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SparUnitTypeDto>>> List([FromQuery] SparUnitTypeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SparUnitTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<SparUnitTypeOption>>> Options([FromQuery] SparUnitTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SparUnitTypeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SparUnitTypeVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SparUnitTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SparUnitTypeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SparUnitTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SparUnitTypeId));
    }

}
