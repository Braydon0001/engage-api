using Engage.Application.Services.ProjectPriorities.Commands;
using Engage.Application.Services.ProjectPriorities.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProjectPriorityController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProjectPriorityDto>>> List([FromQuery] ProjectPriorityListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProjectPriorityDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProjectPriorityOption>>> Options([FromQuery] ProjectPriorityOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectPriorityVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProjectPriorityVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProjectPriorityInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProjectPriorityId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProjectPriorityUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProjectPriorityId));
    }

}
