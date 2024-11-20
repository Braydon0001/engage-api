using Engage.Application.Services.EmployeeFiles.Commands;
using Engage.Application.Services.EmployeeFiles.Queries;

namespace Engage.WebApi.Controllers;

public partial class EmployeeFileController : BaseController
{
    [HttpGet("parentId/{employeeId}")]
    public async Task<ActionResult<ListResult<EmployeeFileDto>>> DtoListByParent([FromQuery] EmployeeFileListQuery query, [FromRoute] int employeeId, CancellationToken cancellationToken)
    {
        query.Id = employeeId;
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<EmployeeFileDto>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeFileVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new EmployeeFileVmQuery { Id = id }, cancellationToken);
        if (entity == null)
        {
            return NotFound();
        }

        return entity;
    }

    [HttpPost]
    public async Task<IActionResult> Insert(EmployeeFileInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.EmployeeFileId));
    }

    //[AllowAnonymous]
    //[HttpPost("migrate")]
    //public async Task<IActionResult> MoveFiles()
    //{
    //    return Ok(await Mediator.Send(new EmployeeFileMigrateFilesCommand { }));
    //}

    [HttpPut]
    public async Task<IActionResult> Update(EmployeeFileUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.EmployeeFileId));
    }

    [HttpPut("file")]
    public async Task<IActionResult> FileUpload([FromForm] EmployeeFileFileUploadCommand command, CancellationToken cancellationToken)
    {
        var file = await Mediator.Send(command, cancellationToken);

        return file == null ? NotFound() : Ok(new OperationStatus(command.Id, file));
    }

    [HttpPut("file/{employeeId}")]
    public async Task<IActionResult> FileuploadForParent([FromForm] EmployeeFileFileUploadCommand command, [FromRoute] int? employeeId, CancellationToken cancellationToken)
    {
        command.EmployeeId = (int)employeeId;
        var file = await Mediator.Send(command, cancellationToken);

        return file == null ? NotFound() : Ok(new OperationStatus(command.Id, file));
    }

    [HttpDelete("file/{employeeId}")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new EmployeeFileFileDeleteCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null,
        }, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(id));
    }

    [HttpDelete("file")]
    public async Task<ActionResult> Delete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new EmployeeFileFileDeleteCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(id));
    }
}