using Engage.Application.Services.ProjectNotes.Commands;
using Engage.Application.Services.ProjectNotes.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProjectNoteController : BaseController
{
    [HttpGet("projectid/{projectId}")]
    public async Task<ActionResult<ListResult<ProjectNoteDto>>> List([FromRoute] ProjectNoteListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProjectNoteDto>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectNoteVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProjectNoteVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProjectNoteInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProjectNoteId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProjectNoteUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProjectNoteId));
    }

    [HttpPut("note")]
    public async Task<IActionResult> UpdateNote(ProjectNoteNoteUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProjectNoteId));
    }

    [HttpDelete("{id}")]
    public async override Task<IActionResult> Delete([FromRoute] int id)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProjectNoteDeleteCommand(id));

        return entity == null ? NotFound() : Ok(new OperationStatus(id));
    }

    [HttpPut("file")]
    public async Task<IActionResult> FileUpload([FromForm] ProjectNoteFileUploadCommand command, CancellationToken cancellationToken)
    {
        var file = await Mediator.Send(command, cancellationToken);

        return file == null ? NotFound() : Ok(new OperationStatus(command.Id, file));
    }

    [HttpDelete("file/{id}/name/{name}")]
    [HttpDelete("file/{id}/name/{name}/type/{type}")]
    public async Task<ActionResult> FileDelete([FromRoute] int id, [FromRoute] string name, [FromRoute] string type, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new ProjectNoteFileDeleteCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(name),
            FileType = !string.IsNullOrWhiteSpace(type) ? HttpUtility.UrlDecode(type) : null
        }, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(id));
    }

}
