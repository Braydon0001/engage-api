using Engage.Application.Services.ProductOrderHistories.Commands;
using Engage.Application.Services.ProductOrderHistories.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProductOrderHistoryController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProductOrderHistoryDto>>> List([FromQuery] ProductOrderHistoryListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProductOrderHistoryDto>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductOrderHistoryVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProductOrderHistoryVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProductOrderHistoryInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProductOrderHistoryId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductOrderHistoryUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProductOrderHistoryId));
    }

}
