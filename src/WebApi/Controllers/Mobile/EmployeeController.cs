using Engage.Application.Services.Employees.Models;
using Engage.Application.Services.Employees.Queries;

namespace Engage.WebApi.Controllers.Mobile;

public class EmployeeController : BaseMobileController
{

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeVm>> GetVm([FromRoute] EmployeeVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [AllowAnonymous]
    [HttpGet("viewmodel/{id}")]
    public async Task<ActionResult<EmployeeVm>> GetViewModel([FromRoute] EmployeeVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}
