using Engage.Application.Services.ProjectTaskComments.Commands;
using Engage.Application.Services.ProjectTaskComments.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProjectTaskCommentController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProjectTaskCommentDto>>> List([FromQuery] ProjectTaskCommentListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProjectTaskCommentDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProjectTaskCommentOption>>> Options([FromQuery] ProjectTaskCommentOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectTaskCommentVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProjectTaskCommentVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProjectTaskCommentInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProjectTaskCommentId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProjectTaskCommentUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProjectTaskCommentId));
    }

}
