using Engage.Application.Services.EmployeeDisciplinaryProcedures.Commands;
using Engage.Application.Services.EmployeeDisciplinaryProcedures.Models;
using Engage.Application.Services.EmployeeDisciplinaryProcedures.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("employee")]
public class EmployeeDisciplinaryProcedureController : BaseController
{

    [HttpGet("employeeId/{employeeId}")]
    public async Task<ActionResult<ListResult<EmployeeDisciplinaryDto>>> GetAllByEmployee([FromRoute] EmployeeDisciplinaryProceduresQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeDisciplinaryProcedureVm>> GetVw([FromRoute] EmployeeDisciplinaryProcedureVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }


    [HttpPost]
    public async Task<IActionResult> Post(CreateEmployeeDisciplinaryProcedureCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateEmployeeDisciplinaryProcedureCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("file")]
    public async Task<IActionResult> FileUpload([FromForm] EmployeeDisciplinaryProcedureFileInsertCommand command, CancellationToken cancellationToken)
    {
        var file = await Mediator.Send(command, cancellationToken);

        return file == null ? NotFound() : Ok(new OperationStatus(command.Id, file));
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new EmployeeDisciplinaryProcedureFileDeleteCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(id));
    }
}
