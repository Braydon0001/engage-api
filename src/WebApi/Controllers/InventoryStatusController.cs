// auto-generated
using Engage.Application.Services.InventoryStatuses.Queries;

namespace Engage.WebApi.Controllers;

public partial class InventoryStatusController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<InventoryStatusDto>>> DtoList([FromQuery]InventoryStatusListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<InventoryStatusDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<InventoryStatusOption>>> OptionList([FromQuery]InventoryStatusOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InventoryStatusVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new InventoryStatusVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }


}