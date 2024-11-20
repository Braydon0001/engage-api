using Engage.Application.Services.EmployeePayRates.Commands;
using Engage.Application.Services.EmployeePayRates.Models;
using Engage.Application.Services.EmployeePayRates.Queries;

namespace Engage.WebApi.Controllers;

public class EmployeePayRateController : BaseController
{
    
    [HttpGet("employeeid/{employeeId}")]
    public async Task<ActionResult<EmployeePayRateVm>> GetVm([FromRoute] EmployeePayRateVmByEmployeeIdQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeePayRateVm>> GetVm([FromRoute] EmployeePayRateVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateEmployeePayRateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateEmployeePayRateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
