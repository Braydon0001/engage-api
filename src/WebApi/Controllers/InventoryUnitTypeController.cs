// auto-generated
using Engage.Application.Services.InventoryUnitTypes.Commands;
using Engage.Application.Services.InventoryUnitTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class InventoryUnitTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<InventoryUnitTypeDto>>> DtoList([FromQuery]InventoryUnitTypeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<InventoryUnitTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<InventoryUnitTypeOption>>> OptionList([FromQuery]InventoryUnitTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InventoryUnitTypeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new InventoryUnitTypeVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(InventoryUnitTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.InventoryUnitTypeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(InventoryUnitTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.InventoryUnitTypeId));
    }


}