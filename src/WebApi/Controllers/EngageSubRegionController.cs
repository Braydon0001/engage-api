using Engage.Application.Services.EngageSubRegions.Commands;
using Engage.Application.Services.EngageSubRegions.Queries;

namespace Engage.WebApi.Controllers;

public partial class EngageSubRegionController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<EngageSubRegionDto>>> List([FromQuery] EngageSubRegionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<EngageSubRegionDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<EngageSubRegionOption>>> Options([FromQuery] EngageSubRegionOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EngageSubRegionVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new EngageSubRegionVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(EngageSubRegionInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.EngageSubRegionId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EngageSubRegionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.EngageSubRegionId));
    }

}
