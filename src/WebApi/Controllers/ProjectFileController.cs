using Engage.Application.Services.ProjectFiles.Commands;
using Engage.Application.Services.ProjectFiles.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProjectFileController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProjectFileDto>>> DtoList([FromQuery] ProjectFileListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProjectFileDto>(entities));
    }

    [HttpGet("parentId/{projectId}")]
    public async Task<ActionResult<ListResult<ProjectFileDto>>> DtoListByParent([FromRoute] ProjectFileListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProjectFileDto>(entities));
    }

    [HttpPost("page")]
    public async Task<ActionResult<ListResult<ProjectFileDto>>> PaginatedQuery(ProjectFilePaginatedQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectFileVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProjectFileVmQuery { Id = id }, cancellationToken);
        if (entity == null)
        {
            return NotFound();
        }

        return entity;
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProjectFileInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProjectFileId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProjectFileUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProjectFileId));
    }

    [HttpPut("file")]
    public async Task<IActionResult> FileUpload([FromForm] ProjectFileFileUploadCommand command, CancellationToken cancellationToken)
    {
        var file = await Mediator.Send(command, cancellationToken);

        return file == null ? NotFound() : Ok(new OperationStatus(command.Id, file));
    }

    [HttpPut("file/{projectId}")]
    public async Task<IActionResult> FileUploadForParent([FromForm] ProjectFileFileUploadCommand command, [FromRoute] int? projectId, CancellationToken cancellationToken)
    {
        command.ProjectId = (int)projectId;
        var file = await Mediator.Send(command, cancellationToken);

        return file == null ? NotFound() : Ok(new OperationStatus(command.Id, file));
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new ProjectFileFileDeleteCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(id));
    }

    [HttpDelete()]
    public async Task<IActionResult> Delete(ProjectFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(command.Id));
    }
}