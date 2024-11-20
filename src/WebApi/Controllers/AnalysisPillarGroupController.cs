using Engage.Application.Services.AnalysisPillarGroups.Commands;
using Engage.Application.Services.AnalysisPillarGroups.Queries;

namespace Engage.WebApi.Controllers;

public partial class AnalysisPillarGroupController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<AnalysisPillarGroupDto>>> List([FromQuery] AnalysisPillarGroupListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<AnalysisPillarGroupDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<AnalysisPillarGroupOption>>> Options([FromQuery] AnalysisPillarGroupOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AnalysisPillarGroupVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new AnalysisPillarGroupVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(AnalysisPillarGroupInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.AnalysisPillarGroupId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(AnalysisPillarGroupUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.AnalysisPillarGroupId));
    }

}
