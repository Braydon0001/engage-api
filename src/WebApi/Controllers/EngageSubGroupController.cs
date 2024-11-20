using Engage.Application.Services.EngageSubGroups.Commands;
using Engage.Application.Services.EngageSubGroups.Models;
using Engage.Application.Services.EngageSubGroups.Queries;

namespace Engage.WebApi.Controllers;

public class EngageSubGroupController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<EngageSubGroupDto>>> GetAll([FromQuery] EngageSubGroupsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("options")]
    public async Task<ActionResult<List<EngageSubGroupOption>>> GetOptions([FromQuery] EngageSubGroupOptionQuery query, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(query, cancellationToken));
    }

    [HttpGet("options/engagegroupIds/{ids}")]
    public async Task<ActionResult<List<EngageSubGroupOption>>> GetOptionsByGroup([FromQuery] EngageSubGroupOptionQuery query, [FromRoute] string ids, CancellationToken cancellationToken)
    {
        query.EngageGroupIds = ids.Split(',').Select(int.Parse).ToList();

        return Ok(await Mediator.Send(query, cancellationToken));
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<EngageSubGroupVm>> GetVm([FromRoute] EngageSubGroupVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEngageSubGroupCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateEngageSubGroupCommand command)
    {
        return Ok(await Mediator.Send(command));
    }


    [HttpDelete("delete/{id}")]
    public override async Task<IActionResult> Delete([FromRoute] int id)
    {

        var command = new DeleteOptionCommand
        {
            Option = OptionDesc.ENGAGESUBGROUPS,
            Id = id,

        };

        return Ok(await Mediator.Send(command));
    }
}
