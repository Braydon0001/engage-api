using Engage.Application.Services.EmployeeRegionContacts.Commands;
using Engage.Application.Services.EmployeeRegionContacts.Models;
using Engage.Application.Services.EmployeeRegionContacts.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("employee")]
public class EmployeeRegionContactController : BaseController
{
    [HttpGet("engageregionId/{engageregionid}")]
    public async Task<ActionResult<ListResult<EmployeeRegionContactDto>>> GetAllByRegion([FromRoute] EmployeeRegionContactsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeRegionContactVm>> GetVm([FromRoute] EmployeeRegionContactVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option/project")]
    public async Task<ActionResult<List<OptionDto>>> GetOptionsByProject([FromQuery] EmployeeRegionContactOptionsByProjectQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeRegionContactCreateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EmployeeRegionContactUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }


    [HttpDelete("{id}")]
    public async override Task<IActionResult> Delete([FromRoute] int id)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new EmployeeRegionContactDeleteCommand(id));

        return entity == null ? NotFound() : Ok(new OperationStatus(id));
    }
}
