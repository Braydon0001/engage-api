using Engage.Application.Services.EmployeeSkills.Commands;
using Engage.Application.Services.EmployeeSkills.Models;
using Engage.Application.Services.EmployeeSkills.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("employee")]
public class EmployeeSkillController : BaseController
{
    [HttpGet("employeeId/{employeeid}")]
    public async Task<ActionResult<ListResult<EmployeeSkillDto>>> GetAllByEmployee([FromRoute] EmployeeSkillsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeSkillVm>> GetVm([FromRoute] EmployeeSkillVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeSkillCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateEmployeeSkillCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("file")]
    public async Task<ActionResult<OperationStatus>> UploadFile([FromForm] EmployeeSkillUploadFileCommand command)
    {
        var result = await Mediator.Send(command);

        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new EmployeeSkillDeleteFileCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result.Status == false ? NotFound() : Ok(new OperationStatus(id));
    }
}
