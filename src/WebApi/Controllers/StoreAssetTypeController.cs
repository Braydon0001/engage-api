using Engage.Application.Services.StoreAssetTypes.Commands;
using Engage.Application.Services.StoreAssetTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class StoreAssetTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<StoreAssetTypeDto>>> GetAll([FromQuery] StoreAssetTypeDtoQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);
        return Ok(new ListResult<StoreAssetTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<ListResult<StoreAssetTypeOption>>> OptionList([FromQuery] StoreAssetTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);
        return Ok(entities);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<StoreAssetTypeVm>> GetVm([FromRoute] StoreAssetTypeVmQuery query, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(query, cancellationToken));
    }

    [HttpPost]
    public async Task<IActionResult> Insert(StoreAssetTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.Id));
    }

    [HttpPut]
    public async Task<IActionResult> Update(StoreAssetTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.Id));
    }
}
