// auto-generated
using Engage.Application.Services.InventoryWarehouses.Commands;
using Engage.Application.Services.InventoryWarehouses.Queries;

namespace Engage.WebApi.Controllers;

public partial class InventoryWarehouseController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<InventoryWarehouseDto>>> DtoList([FromQuery]InventoryWarehouseListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<InventoryWarehouseDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<InventoryWarehouseOption>>> OptionList([FromQuery]InventoryWarehouseOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InventoryWarehouseVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new InventoryWarehouseVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(InventoryWarehouseInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.InventoryWarehouseId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(InventoryWarehouseUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.InventoryWarehouseId));
    }


}