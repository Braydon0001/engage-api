using Engage.Application.Services.EngageGroups.Commands;
using Engage.Application.Services.EngageGroups.Models;
using Engage.Application.Services.EngageGroups.Queries;

namespace Engage.WebApi.Controllers;

public class EngageGroupController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<EngageGroupDto>>> GetAll([FromQuery] EngageGroupsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("hierarchy")]
    public async Task<ActionResult<ListResult<EngageGroupDto>>> GetHierarchy([FromQuery] EngageGroupHierarchyQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EngageGroupVm>> GetVm([FromRoute] EngageGroupVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEngageGroupCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateEngageGroupCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
