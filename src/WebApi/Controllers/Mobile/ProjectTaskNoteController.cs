using Engage.Application.Services.ProjectTaskNotes.Commands;
using Engage.Application.Services.ProjectTaskNotes.Queries;

namespace Engage.WebApi.Controllers.Mobile;

public partial class ProjectTaskNoteController : BaseMobileController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProjectTaskNoteDto>>> List([FromQuery] ProjectTaskNoteListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProjectTaskNoteDto>(entities));
    }

    [HttpGet("projecttaskid/{projecttaskid}")]
    public async Task<ActionResult<ListResult<ProjectTaskNoteDto>>> ListByProject([FromRoute] ProjectTaskNoteListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProjectTaskNoteDto>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectTaskNoteVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProjectTaskNoteVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProjectTaskNoteInsertCommand command, CancellationToken cancellationToken)
    {
        var opStatus = await Mediator.Send(command, cancellationToken);

        return Ok(opStatus);
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProjectTaskNoteUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProjectTaskNoteId));
    }

    [HttpPut("note")]
    public async Task<IActionResult> UpdateNote(ProjectTaskNoteNoteUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProjectTaskNoteId));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProjectTaskNoteDeleteCommand(id));

        return entity == null ? NotFound() : Ok(new OperationStatus(id));
    }

    [HttpPut("file")]
    public async Task<IActionResult> FileUpload([FromForm] ProjectTaskNoteFileUploadCommand command, CancellationToken cancellationToken)
    {
        var file = await Mediator.Send(command, cancellationToken);

        return file == null ? NotFound() : Ok(new OperationStatus(command.Id, file));
    }

    [HttpDelete("file/{id}/name/{name}")]
    [HttpDelete("file/{id}/name/{name}/type/{type}")]
    public async Task<ActionResult> FileDelete([FromRoute] int id, [FromRoute] string name, [FromRoute] string type, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new ProjectTaskNoteFileDeleteCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(name),
            FileType = !string.IsNullOrWhiteSpace(type) ? HttpUtility.UrlDecode(type) : null
        }, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(id));
    }

}
