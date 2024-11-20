// auto-generated
using Engage.Application.Services.InventoryGroups.Commands;
using Engage.Application.Services.InventoryGroups.Queries;

namespace Engage.WebApi.Controllers;

public partial class InventoryGroupController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<InventoryGroupDto>>> DtoList([FromQuery]InventoryGroupListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<InventoryGroupDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<InventoryGroupOption>>> OptionList([FromQuery]InventoryGroupOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InventoryGroupVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new InventoryGroupVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(InventoryGroupInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.InventoryGroupId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(InventoryGroupUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.InventoryGroupId));
    }


}