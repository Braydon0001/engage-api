using Engage.Application.Services.ProductPrices.Commands;
using Engage.Application.Services.ProductPrices.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProductPriceController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProductPriceDto>>> List([FromQuery] ProductPriceListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProductPriceDto>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductPriceVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProductPriceVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpGet("product/{id}")]
    public async Task<ActionResult<ProductPriceByProductVm>> VmByProduct([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        return Ok(await Mediator.Send(new ProductPriceByProductVmQuery { Id = id }, cancellationToken));
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProductPriceInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProductPriceId));
    }

}
