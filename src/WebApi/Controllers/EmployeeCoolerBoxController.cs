using Engage.Application.Services.EmployeeCoolerBoxes.Commands;
using Engage.Application.Services.EmployeeCoolerBoxes.Models;
using Engage.Application.Services.EmployeeCoolerBoxes.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("Employee")]
public class EmployeeCoolerBoxController : BaseController
{
    [HttpGet()]
    public async Task<ActionResult<ListResult<EmployeeCoolerBoxDto>>> GetAll([FromQuery] EmployeeCoolerBoxesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("EmployeeId/{Employeeid}")]
    public async Task<ActionResult<ListResult<EmployeeCoolerBoxDto>>> GetByEmployee([FromQuery] EmployeeCoolerBoxesQuery query, [FromRoute] int EmployeeId)
    {
        query.EmployeeId = EmployeeId;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option/names")]
    public async Task<ActionResult<List<OptionDto>>> GetOptions([FromQuery] EmployeeCoolerBoxOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeCoolerBoxVm>> GetVm([FromRoute] EmployeeCoolerBoxVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeCoolerBoxCreateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EmployeeCoolerBoxUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("reassign")]
    public async Task<IActionResult> Reassign(EmployeeCoolerBoxReassignCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("reassign/bulk")]
    public async Task<IActionResult> ReassignBulk(EmployeeCoolerBoxBulkReassignCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("file")]
    public async Task<ActionResult<OperationStatus>> UploadFile([FromForm] EmployeeCoolerBoxUploadFileCommand command)
    {
        var result = await Mediator.Send(command);

        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new EmployeeCoolerBoxDeleteFileCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result.Status == false ? NotFound() : Ok(new OperationStatus(id));
    }
}
