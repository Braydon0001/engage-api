using Engage.Application.Services.SparAnalysisGroups.Commands;
using Engage.Application.Services.SparAnalysisGroups.Queries;

namespace Engage.WebApi.Controllers;

public partial class SparAnalysisGroupController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SparAnalysisGroupDto>>> List([FromQuery] SparAnalysisGroupListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SparAnalysisGroupDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<SparAnalysisGroupOption>>> Options([FromQuery] SparAnalysisGroupOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SparAnalysisGroupVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SparAnalysisGroupVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SparAnalysisGroupInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SparAnalysisGroupId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SparAnalysisGroupUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SparAnalysisGroupId));
    }

}
