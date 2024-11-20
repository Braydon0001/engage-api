// auto-generated
using Engage.Application.Services.StoreAssetSubTypes.Commands;
using Engage.Application.Services.StoreAssetSubTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class StoreAssetSubTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<StoreAssetSubTypeDto>>> DtoList([FromQuery] StoreAssetSubTypeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<StoreAssetSubTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<StoreAssetSubTypeOption>>> OptionList([FromQuery] StoreAssetSubTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StoreAssetSubTypeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new StoreAssetSubTypeVmQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(StoreAssetSubTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.StoreAssetSubTypeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(StoreAssetSubTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.StoreAssetSubTypeId));
    }


}