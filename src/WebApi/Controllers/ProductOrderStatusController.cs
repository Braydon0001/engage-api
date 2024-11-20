using Engage.Application.Services.ProductOrderStatuses.Commands;
using Engage.Application.Services.ProductOrderStatuses.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProductOrderStatusController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProductOrderStatusDto>>> List([FromQuery] ProductOrderStatusListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProductOrderStatusDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProductOrderStatusOption>>> Options([FromQuery] ProductOrderStatusOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductOrderStatusVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProductOrderStatusVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProductOrderStatusInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProductOrderStatusId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductOrderStatusUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProductOrderStatusId));
    }

}
