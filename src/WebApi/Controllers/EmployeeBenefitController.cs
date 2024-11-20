using Engage.Application.Services.EmployeeBenefits.Commands;
using Engage.Application.Services.EmployeeBenefits.Models;
using Engage.Application.Services.EmployeeBenefits.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("employee")]
public class EmployeeBenefitController : BaseController
{
    
    [HttpGet("employeeId/{employeeId}")]
    public async Task<ActionResult<ListResult<EmployeeBenefitDto>>> GetAllByEmployee([FromRoute] EmployeeBenefitsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }    

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeBenefitVm>> GetVm([FromRoute] EmployeeBenefitVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeBenefitCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateEmployeeBenefitCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
