using Engage.Application.Services.ProjectAssignees.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProjectAssigneeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProjectAssigneeDto>>> List([FromQuery] ProjectAssigneeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProjectAssigneeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProjectAssigneeOption>>> Options([FromQuery] ProjectAssigneeOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectAssigneeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProjectAssigneeVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }
}
