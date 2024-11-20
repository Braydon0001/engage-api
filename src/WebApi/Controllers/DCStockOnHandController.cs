using Engage.Application.Services.DCStockOnHands.Commands;
using Engage.Application.Services.DCStockOnHands.Queries;

namespace Engage.WebApi.Controllers;

public partial class DCStockOnHandController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<DCStockOnHandDto>>> List([FromQuery] DCStockOnHandListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<DCStockOnHandDto>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DCStockOnHandVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new DCStockOnHandVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(DCStockOnHandInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.DCStockOnHandId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(DCStockOnHandUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.DCStockOnHandId));
    }

}
