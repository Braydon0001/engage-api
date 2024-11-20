using Engage.Application.Services.EmployeeWorkRoleContacts.Commands;
using Engage.Application.Services.EmployeeWorkRoleContacts.Models;
using Engage.Application.Services.EmployeeWorkRoleContacts.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("employee")]
public class EmployeeWorkRoleContactController : BaseController
{
    [HttpGet("employeeid/{employeeid}")]
    public async Task<ActionResult<ListResult<EmployeeWorkRoleContactDto>>> GetAllByRegion([FromRoute] EmployeeWorkRoleContactsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeWorkRoleContactVm>> GetVm([FromRoute] EmployeeWorkRoleContactVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeWorkRoleContactCreateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EmployeeWorkRoleContactUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
