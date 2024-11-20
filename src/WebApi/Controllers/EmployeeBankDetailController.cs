using Engage.Application.Services.EmployeeBankDetails.Commands;
using Engage.Application.Services.EmployeeBankDetails.Models;
using Engage.Application.Services.EmployeeBankDetails.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("employee")]
public class EmployeeBankDetailController : BaseController
{
    [HttpGet("employeeId/{employeeid}")]
    public async Task<ActionResult<ListResult<EmployeeBankDetailDto>>> GetByEmployee([FromRoute] EmployeeBankDetailsQuery query, [FromQuery] bool? IsPrimary)
    {
        query.IsPrimary = IsPrimary;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeBankDetailVm>> GetVm([FromRoute] EmployeeBankDetailVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeBankDetailCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateEmployeeBankDetailCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("file")]
    public async Task<ActionResult<OperationStatus>> UploadFile([FromForm] EmployeeBankDetailUploadFileCommand command)
    {
        var result = await Mediator.Send(command);

        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new EmployeeBankDetailDeleteFileCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result.Status == false ? NotFound() : Ok(new OperationStatus(id));
    }
}
