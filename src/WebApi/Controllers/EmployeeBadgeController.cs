using Engage.Application.Services.EmployeeBadges.Commands;
using Engage.Application.Services.EmployeeBadges.Models;
using Engage.Application.Services.EmployeeBadges.Queries;

namespace Engage.WebApi.Controllers;

public class EmployeeBadgeController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<ActionResult<EmployeeBadgeDto>> GetByBadge([FromRoute] EmployeeBadgeVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    public async Task<ActionResult<ListResult<EmployeeBadgeDto>>> GetAll([FromRoute] EmployeeBadgeQuery query)
    {
        return Ok(new ListResult<EmployeeBadgeDto>(await Mediator.Send(query)));
    }

    [HttpGet("option")]
    public async Task<ActionResult<List<OptionDto>>> EmployeeBadgeOptionsQuery([FromRoute] EmployeeBadgeOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeBadgeCreateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EmployeeBadgeUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
