using Engage.Application.Services.ProjectStatuses.Commands;
using Engage.Application.Services.ProjectStatuses.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProjectStatusController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProjectStatusDto>>> List([FromQuery] ProjectStatusListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProjectStatusDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProjectStatusOption>>> Options([FromQuery] ProjectStatusOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectStatusVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProjectStatusVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProjectStatusInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProjectStatusId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProjectStatusUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProjectStatusId));
    }

}
