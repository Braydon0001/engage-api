using Engage.Application.Services.ProjectTaskSeverities.Commands;
using Engage.Application.Services.ProjectTaskSeverities.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProjectTaskSeverityController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProjectTaskSeverityDto>>> List([FromQuery] ProjectTaskSeverityListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProjectTaskSeverityDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProjectTaskSeverityOption>>> Options([FromQuery] ProjectTaskSeverityOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectTaskSeverityVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProjectTaskSeverityVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProjectTaskSeverityInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProjectTaskSeverityId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProjectTaskSeverityUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProjectTaskSeverityId));
    }

}
