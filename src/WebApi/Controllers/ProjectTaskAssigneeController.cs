using Engage.Application.Services.ProjectTaskAssignees.Commands;
using Engage.Application.Services.ProjectTaskAssignees.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProjectTaskAssigneeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProjectTaskAssigneeDto>>> List([FromQuery] ProjectTaskAssigneeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProjectTaskAssigneeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProjectTaskAssigneeOption>>> Options([FromQuery] ProjectTaskAssigneeOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectTaskAssigneeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProjectTaskAssigneeVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProjectTaskAssigneeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProjectTaskAssigneeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProjectTaskAssigneeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProjectTaskAssigneeId));
    }

}
