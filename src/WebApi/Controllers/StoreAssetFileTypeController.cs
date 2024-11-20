using Engage.Application.Services.StoreAssetFileTypes.Commands;
using Engage.Application.Services.StoreAssetFileTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class StoreAssetFileTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<StoreAssetFileTypeDto>>> List([FromQuery] StoreAssetFileTypeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<StoreAssetFileTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<StoreAssetFileTypeOption>>> Options([FromQuery] StoreAssetFileTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StoreAssetFileTypeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new StoreAssetFileTypeVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(StoreAssetFileTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.StoreAssetFileTypeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(StoreAssetFileTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.StoreAssetFileTypeId));
    }

}
