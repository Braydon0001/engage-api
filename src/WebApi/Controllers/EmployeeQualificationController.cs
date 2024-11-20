using Engage.Application.Services.EmployeeQualifications.Commands;
using Engage.Application.Services.EmployeeQualifications.Models;
using Engage.Application.Services.EmployeeQualifications.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("employee")]
public class EmployeeQualificationController : BaseController
{
    [HttpGet("employeeId/{employeeid}")]
    public async Task<ActionResult<ListResult<EmployeeQualificationDto>>> GetAllByEmployee([FromRoute] EmployeeQualificationsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeQualificationVm>> GetVm([FromRoute] EmployeeQualificationVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeQualificationCreateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EmployeeQualificationUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }


    [HttpPut("file")]
    public async Task<ActionResult<OperationStatus>> UploadFile([FromForm] EmployeeQualificationUploadFileCommand command)
    {
        var result = await Mediator.Send(command);

        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new EmployeeQualificationDeleteFileCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result.Status == false ? NotFound() : Ok(new OperationStatus(id));
    }
}
