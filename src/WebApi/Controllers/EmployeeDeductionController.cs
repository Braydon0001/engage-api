using Engage.Application.Services.EmployeeDeductions.Commands;
using Engage.Application.Services.EmployeeDeductions.Models;
using Engage.Application.Services.EmployeeDeductions.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("employee")]
public class EmployeeDeductionController : BaseController
{
    

    [HttpGet("employeeId/{employeeId}")]
    public async Task<ActionResult<ListResult<EmployeeDeductionDto>>> GetByEmployee([FromRoute] EmployeeDeductionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeDeductionVm>> GetVm([FromRoute] GetEmployeeDeductionVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeDeductionCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateEmployeeDeductionCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
