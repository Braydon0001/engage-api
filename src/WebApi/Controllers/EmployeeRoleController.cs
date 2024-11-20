using Engage.Application.Services.EmployeeWorkRoles.Commands;
using Engage.Application.Services.EmployeeWorkRoles.Models;
using Engage.Application.Services.EmployeeWorkRoles.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("employee")]
public class EmployeeRoleController : BaseController
{
    [HttpGet("employeeId/{employeeid}")]
    public async Task<ActionResult<ListResult<EmployeeWorkRoleDto>>> GetAllByEmployee([FromRoute] EmployeeWorkRolesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeWorkRoleVm>> GetVm([FromRoute] EmployeeWorkRoleVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option/{employeeId}")]
    public async Task<ActionResult<List<OptionDto>>> GetOptionsByEmployee([FromQuery] EmployeeWorkRoleOptionsQuery query, [FromRoute] int EmployeeId)
    {
        query.EmployeeId = EmployeeId;
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeWorkRoleCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateEmployeeWorkRoleCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
