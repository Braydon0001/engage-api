using Engage.Application.Services.EmployeeLoans.Commands;
using Engage.Application.Services.EmployeeLoans.Models;
using Engage.Application.Services.EmployeeLoans.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("employee")]
public class EmployeeLoanController : BaseController
{
    
    [HttpGet("employeeId/{employeeId}")]
    public async Task<ActionResult<ListResult<EmployeeLoanDto>>> GetByEmployee([FromRoute] EmployeeLoansQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeLoanVm>> GetVm([FromRoute] EmployeeLoanVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeLoanCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateEmployeeLoanCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
