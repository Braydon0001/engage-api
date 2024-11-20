// auto-generated
using Engage.Application.Services.ProductSizeTypes.Commands;
using Engage.Application.Services.ProductSizeTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProductSizeTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProductSizeTypeDto>>> DtoList([FromQuery]ProductSizeTypeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProductSizeTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProductSizeTypeOption>>> OptionList([FromQuery]ProductSizeTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductSizeTypeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProductSizeTypeVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProductSizeTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProductSizeTypeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductSizeTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProductSizeTypeId));
    }


}