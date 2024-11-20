using Engage.Application.Services.EmployeeExpenseClaims.Commands;
using Engage.Application.Services.EmployeeExpenseClaims.Models;
using Engage.Application.Services.EmployeeExpenseClaims.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("employee")]
public class EmployeeClaimController : BaseController
{

    [HttpGet("employeeId/{employeeId}")]
    public async Task<ActionResult<ListResult<EmployeeExpenseClaimDto>>> GetByEmployee([FromRoute] EmployeeExpenseClaimsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeExpenseClaimVm>> GetVm([FromRoute] GetEmployeeExpenseClaimVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeExpenseClaimCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateEmployeeExpenseClaimCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
