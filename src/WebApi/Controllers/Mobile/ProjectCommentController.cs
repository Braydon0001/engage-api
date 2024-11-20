using Engage.Application.Services.ProjectComments.Commands;
using Engage.Application.Services.ProjectComments.Queries;

namespace Engage.WebApi.Controllers.Mobile;

public partial class ProjectCommentController : BaseMobileController
{
    [HttpGet("project/{id}")]
    public async Task<ActionResult<List<ProjectCommentDto>>> List([FromQuery] ProjectCommentListQuery query, [FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }
        query.ProjectId = id;
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("offline")]
    public async Task<ActionResult<List<ProjectCommentDto>>> OfflineList([FromQuery] ProjectCommentListOfflineQuery query, CancellationToken cancellationToken)
    {

        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProjectCommentOption>>> Options([FromQuery] ProjectCommentOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectCommentVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProjectCommentVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProjectCommentInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProjectCommentId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProjectCommentUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProjectCommentId));
    }

    [HttpPut("file")]
    public async Task<IActionResult> FileUpload([FromForm] ProjectCommentFileUploadCommand command, CancellationToken cancellationToken)
    {
        var file = await Mediator.Send(command, cancellationToken);

        return file == null ? NotFound() : Ok(new OperationStatus(command.Id, file));
    }

    [HttpDelete("file/{id}/name/{name}")]
    [HttpDelete("file/{id}/name/{name}/type/{type}")]
    public async Task<ActionResult> FileDelete([FromRoute] int id, [FromRoute] string name, [FromRoute] string type, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new ProjectCommentFileDeleteCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(name),
            FileType = !string.IsNullOrWhiteSpace(type) ? HttpUtility.UrlDecode(type) : null
        }, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(id));
    }

}
