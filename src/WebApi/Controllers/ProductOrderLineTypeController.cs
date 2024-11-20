using Engage.Application.Services.ProductOrderLineTypes.Commands;
using Engage.Application.Services.ProductOrderLineTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProductOrderLineTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProductOrderLineTypeDto>>> List([FromQuery] ProductOrderLineTypeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProductOrderLineTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProductOrderLineTypeOption>>> Options([FromQuery] ProductOrderLineTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductOrderLineTypeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProductOrderLineTypeVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProductOrderLineTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProductOrderLineTypeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductOrderLineTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProductOrderLineTypeId));
    }

}
