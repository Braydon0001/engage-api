using Engage.Application.Services.OrderStagingSkus.Commands;
using Engage.Application.Services.OrderStagingSkus.Queries;

namespace Engage.WebApi.Controllers;

public partial class OrderStagingSkuController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<OrderStagingSkuDto>>> List([FromQuery] OrderStagingSkuListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<OrderStagingSkuDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<OrderStagingSkuOption>>> Options([FromQuery] OrderStagingSkuOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderStagingSkuVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new OrderStagingSkuVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(OrderStagingSkuInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.OrderStagingSkuId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(OrderStagingSkuUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.OrderStagingSkuId));
    }

}
