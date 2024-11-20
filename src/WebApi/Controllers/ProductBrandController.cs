// auto-generated
using Engage.Application.Services.ProductBrands.Commands;
using Engage.Application.Services.ProductBrands.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProductBrandController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProductBrandDto>>> DtoList([FromQuery]ProductBrandListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProductBrandDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProductBrandOption>>> OptionList([FromQuery]ProductBrandOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductBrandVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProductBrandVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProductBrandInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProductBrandId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductBrandUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProductBrandId));
    }


}