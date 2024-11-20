using Engage.Application.Services.ProjectTacOps.Commands;
using Engage.Application.Services.ProjectTacOps.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProjectTacOpController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProjectTacOpDto>>> List([FromQuery] ProjectTacOpListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProjectTacOpDto>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectTacOpVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProjectTacOpVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProjectTacOpInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProjectTacOpId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProjectTacOpUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProjectTacOpId));
    }
}
