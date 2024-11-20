// auto-generated
using Engage.Application.Services.InventoryPeriods.Commands;
using Engage.Application.Services.InventoryPeriods.Queries;

namespace Engage.WebApi.Controllers;

public partial class InventoryPeriodController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<InventoryPeriodDto>>> DtoList([FromQuery] InventoryPeriodListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<InventoryPeriodDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<InventoryPeriodOption>>> OptionList([FromQuery] InventoryPeriodOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InventoryPeriodVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new InventoryPeriodVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(InventoryPeriodInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.InventoryPeriodId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(InventoryPeriodUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.InventoryPeriodId));
    }


}