// auto-generated
using Engage.Application.Services.ProductWarehouses.Commands;
using Engage.Application.Services.ProductWarehouses.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProductWarehouseController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProductWarehouseDto>>> DtoList([FromQuery]ProductWarehouseListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProductWarehouseDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProductWarehouseOption>>> OptionList([FromQuery]ProductWarehouseOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductWarehouseVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProductWarehouseVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProductWarehouseInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProductWarehouseId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductWarehouseUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProductWarehouseId));
    }


}