using Engage.Application.Services.EmployeeLeaveEntries.Commands;
using Engage.Application.Services.EmployeeLeaveEntries.Models;
using Engage.Application.Services.EmployeeLeaveEntries.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("employee")]
public class EmployeeLeaveEntryController : BaseController
{

    [HttpGet()]
    public async Task<ActionResult<ListResult<EmployeeLeaveEntryDto>>> DtoList([FromQuery] EmployeeLeaveEntriesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeLeaveEntryVm>> Vm([FromRoute] EmployeeLeaveEntryVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Insert(CreateEmployeeLeaveEntryCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateEmployeeLeaveEntryCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
