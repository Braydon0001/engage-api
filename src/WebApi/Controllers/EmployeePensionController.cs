using Engage.Application.Services.EmployeePensions.Commands;
using Engage.Application.Services.EmployeePensions.Queries;

namespace Engage.WebApi.Controllers;

public partial class EmployeePensionController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<EmployeePensionDto>>> DtoList([FromQuery] EmployeePensionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<EmployeePensionDto>(entities));
    }

    [HttpPost]
    public async Task<IActionResult> Insert(EmployeePensionInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.EmployeePensionId));
    }

    [HttpPost("next")]
    public async Task<IActionResult> InsertNext([FromForm] EmployeePensionNextInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.EmployeePensionId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EmployeePensionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.EmployeePensionId));
    }

    [HttpPut("next")]
    public async Task<IActionResult> UpdateNext([FromForm] EmployeePensionNextUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.EmployeePensionId));
    }

    [HttpPut("file")]
    public async Task<IActionResult> FileUpload([FromForm] EmployeePensionFileUploadCommand command, CancellationToken cancellationToken)
    {
        var file = await Mediator.Send(command, cancellationToken);

        return file == null ? NotFound() : Ok(new OperationStatus(command.Id, file));
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new EmployeePensionFileDeleteCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(id));
    }

}