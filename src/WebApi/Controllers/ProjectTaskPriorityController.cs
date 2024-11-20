using Engage.Application.Services.ProjectTaskPriorities.Commands;
using Engage.Application.Services.ProjectTaskPriorities.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProjectTaskPriorityController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProjectTaskPriorityDto>>> List([FromQuery] ProjectTaskPriorityListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProjectTaskPriorityDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProjectTaskPriorityOption>>> Options([FromQuery] ProjectTaskPriorityOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectTaskPriorityVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProjectTaskPriorityVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProjectTaskPriorityInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProjectTaskPriorityId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProjectTaskPriorityUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProjectTaskPriorityId));
    }

}
