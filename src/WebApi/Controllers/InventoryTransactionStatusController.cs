// auto-generated
using Engage.Application.Services.InventoryTransactionStatuses.Queries;

namespace Engage.WebApi.Controllers;

public partial class InventoryTransactionStatusController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<InventoryTransactionStatusDto>>> DtoList([FromQuery]InventoryTransactionStatusListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<InventoryTransactionStatusDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<InventoryTransactionStatusOption>>> OptionList([FromQuery]InventoryTransactionStatusOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InventoryTransactionStatusVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new InventoryTransactionStatusVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }


}