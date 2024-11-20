using Engage.Application.Services.ProjectTaskTypes.Commands;
using Engage.Application.Services.ProjectTaskTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProjectTaskTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProjectTaskTypeDto>>> List([FromQuery] ProjectTaskTypeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProjectTaskTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProjectTaskTypeOption>>> Options([FromQuery] ProjectTaskTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectTaskTypeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProjectTaskTypeVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProjectTaskTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProjectTaskTypeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProjectTaskTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProjectTaskTypeId));
    }

}
