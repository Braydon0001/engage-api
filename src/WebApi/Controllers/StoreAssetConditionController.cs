// auto-generated
using Engage.Application.Services.StoreAssetConditions.Commands;
using Engage.Application.Services.StoreAssetConditions.Queries;

namespace Engage.WebApi.Controllers;

public partial class StoreAssetConditionController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<StoreAssetConditionDto>>> DtoList([FromQuery] StoreAssetConditionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<StoreAssetConditionDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<StoreAssetConditionOption>>> OptionList([FromQuery] StoreAssetConditionOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StoreAssetConditionVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new StoreAssetConditionVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(StoreAssetConditionInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.StoreAssetConditionId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(StoreAssetConditionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.StoreAssetConditionId));
    }


}