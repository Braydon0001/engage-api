using Engage.Application.Services.AnalysisPillarSubGroups.Commands;
using Engage.Application.Services.AnalysisPillarSubGroups.Queries;

namespace Engage.WebApi.Controllers;

public partial class AnalysisPillarSubGroupController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<AnalysisPillarSubGroupDto>>> List([FromQuery] AnalysisPillarSubGroupListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<AnalysisPillarSubGroupDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<AnalysisPillarSubGroupOption>>> Options([FromQuery] AnalysisPillarSubGroupOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AnalysisPillarSubGroupVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new AnalysisPillarSubGroupVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(AnalysisPillarSubGroupInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.AnalysisPillarSubGroupId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(AnalysisPillarSubGroupUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.AnalysisPillarSubGroupId));
    }

}
