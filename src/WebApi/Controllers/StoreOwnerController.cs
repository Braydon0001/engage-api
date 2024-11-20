// auto-generated
using Engage.Application.Services.StoreOwners.Commands;
using Engage.Application.Services.StoreOwners.Queries;

namespace Engage.WebApi.Controllers;

public partial class StoreOwnerController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<StoreOwnerDto>>> DtoList([FromQuery] StoreOwnerListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<StoreOwnerDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<StoreOwnerOption>>> OptionList([FromQuery] StoreOwnerOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StoreOwnerVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new StoreOwnerVmQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(StoreOwnerInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.StoreOwnerId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(StoreOwnerUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.StoreOwnerId));
    }


}