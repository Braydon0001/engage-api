using Engage.Application.Services.EmployeeAddresses.Commands;
using Engage.Application.Services.EmployeeAddresses.Models;
using Engage.Application.Services.EmployeeAddresses.Queries;

namespace Engage.WebApi.Controllers;

public class EmployeeAddressController : BaseController
{
    
    [HttpGet("employeeid/{employeeId}")]
    public async Task<ActionResult<EmployeeAddressVm>> GetVm([FromRoute] EmployeeAddressVmByEmployeeIdQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeAddressVm>> GetVm([FromRoute] EmployeeAddressVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateEmployeeAddressCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateEmployeeAddressCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
