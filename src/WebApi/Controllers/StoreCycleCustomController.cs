using Engage.Application.Services.StoreCycles.Commands;
using Engage.Application.Services.StoreCycles.Queries;

namespace Engage.WebApi.Controllers;

public partial class StoreCycleController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<StoreCycleDto>>> DtoList([FromQuery] StoreCycleListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<StoreCycleDto>(entities));
    }

    [HttpGet("store/{storeid}")]
    public async Task<ActionResult<ListResult<StoreCycleDto>>> StoreById([FromRoute] int storeId, CancellationToken cancellationToken)
    {
        if (storeId <= 0)
        {
            return BadRequest(BadIdText);
        }
        var entities = await Mediator.Send(new StoreCycleByStoreQuery { StoreId = storeId }, cancellationToken);

        return Ok(new ListResult<StoreCycleDto>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StoreCycleVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new StoreCycleVmQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(StoreCycleInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.StoreCycleId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(StoreCycleUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.StoreCycleId));
    }
}
