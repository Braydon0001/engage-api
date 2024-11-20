// auto-generated
using Engage.Application.Services.InventoryTransactionTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class InventoryTransactionTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<InventoryTransactionTypeDto>>> DtoList([FromQuery]InventoryTransactionTypeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<InventoryTransactionTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<InventoryTransactionTypeOption>>> OptionList([FromQuery]InventoryTransactionTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InventoryTransactionTypeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new InventoryTransactionTypeVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }


}