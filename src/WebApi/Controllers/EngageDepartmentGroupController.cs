using Engage.Application.Services.EngageDepartmentGroups.Commands;
using Engage.Application.Services.EngageDepartmentGroups.Models;
using Engage.Application.Services.EngageDepartmentGroups.Queries;

namespace Engage.WebApi.Controllers;

public class EngageDepartmentGroupController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<EngageDepartmentGroupDto>>> GetAll([FromQuery] EngageDepartmentGroupsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EngageDepartmentGroupVm>> GetVm([FromRoute] EngageDepartmentGroupVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(EngageDepartmentGroupCreateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EngageDepartmentGroupUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
