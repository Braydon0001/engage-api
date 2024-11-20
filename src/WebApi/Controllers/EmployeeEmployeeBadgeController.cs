using Engage.Application.Services.EmployeeEmployeeBadges.Commands;
using Engage.Application.Services.EmployeeEmployeeBadges.Models;
using Engage.Application.Services.EmployeeEmployeeBadges.Queries;

namespace Engage.WebApi.Controllers;

public class EmployeeEmployeeBadgeController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<ActionResult<ListResult<EmployeeEmployeeBadgeDto>>> GetByBadge([FromRoute] EmployeeEmployeeBadgeQuery query)
    {
        return Ok(new ListResult<EmployeeEmployeeBadgeDto>(await Mediator.Send(query)));
    }

    [HttpGet]
    public async Task<ActionResult<ListResult<EmployeeEmployeeBadgeDto>>> GetAll([FromRoute] EmployeeEmployeeBadgeQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeEmployeeBadgeCreateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EmployeeEmployeeBadgeUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{EmployeeId}/{EmployeeBadgeId}")]
    public async Task<IActionResult> Delete([FromRoute] EmployeeEmployeeBadgeRemoveCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
