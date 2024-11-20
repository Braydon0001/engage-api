// auto-generated
using Engage.Application.Services.ProductManufacturers.Commands;
using Engage.Application.Services.ProductManufacturers.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProductManufacturerController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProductManufacturerDto>>> DtoList([FromQuery]ProductManufacturerListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProductManufacturerDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProductManufacturerOption>>> OptionList([FromQuery]ProductManufacturerOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductManufacturerVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProductManufacturerVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProductManufacturerInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProductManufacturerId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductManufacturerUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProductManufacturerId));
    }


}