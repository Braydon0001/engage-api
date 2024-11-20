using Engage.Application.Services.TrainingFiles.Commands;
using Engage.Application.Services.TrainingFiles.Queries;

namespace Engage.WebApi.Controllers;

public partial class TrainingFileController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<TrainingFileDto>>> DtoList([FromQuery] TrainingFileListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<TrainingFileDto>(entities));
    }

    [HttpGet("parentId/{trainingId}")]
    public async Task<ActionResult<ListResult<TrainingFileDto>>> DtoListByParent([FromRoute] TrainingFileListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<TrainingFileDto>(entities));
    }

    [HttpPost("page")]
    public async Task<ActionResult<ListResult<TrainingFileDto>>> PaginatedQuery(TrainingFilePaginatedQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TrainingFileVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new TrainingFileVmQuery { Id = id }, cancellationToken);
        if (entity == null)
        {
            return NotFound();
        }

        return entity;
    }

    [HttpPost]
    public async Task<IActionResult> Insert(TrainingFileInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.TrainingFileId));
    }

    [AllowAnonymous]
    [HttpPost("movefiles")]
    public async Task<IActionResult> MoveFiles()
    {
        return Ok(await Mediator.Send(new MoveFilesToNewTableCommand { }));
    }

    [HttpPut]
    public async Task<IActionResult> Update(TrainingFileUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.TrainingFileId));
    }

    [HttpPut("file")]
    public async Task<IActionResult> FileUpload([FromForm] TrainingFileFileUploadCommand command, CancellationToken cancellationToken)
    {
        var file = await Mediator.Send(command, cancellationToken);

        return file == null ? NotFound() : Ok(new OperationStatus(command.Id, file));
    }

    [HttpPut("file/{trainingId}")]
    public async Task<IActionResult> FileUploadForParent([FromForm] TrainingFileFileUploadCommand command, [FromRoute] int? trainingId, CancellationToken cancellationToken)
    {
        command.TrainingId = (int)trainingId;
        var file = await Mediator.Send(command, cancellationToken);

        return file == null ? NotFound() : Ok(new OperationStatus(command.Id, file));
    }

    [HttpDelete("file/{trainingId}")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new TrainingFileFileDeleteCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(id));
    }

    [HttpDelete()]
    public async Task<IActionResult> Delete(TrainingFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(command.Id));
    }
}