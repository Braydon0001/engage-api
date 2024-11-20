using Engage.Application.Services.EngageRegions.Commands;
using Engage.Application.Services.EngageRegions.Models;
using Engage.Application.Services.EngageRegions.Queries;

namespace Engage.WebApi.Controllers;

public class EngageRegionController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<PaginatedListResult<EngageRegionDto>>> GetAll([FromQuery] EngageRegionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option")]
    public async Task<ActionResult<PaginatedListResult<OptionDto>>> GetOptions([FromQuery] EngageRegionOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option/{id}")]
    public async Task<ActionResult<OptionDto>> GetOptionVm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }
        return Ok(await Mediator.Send(new EngageRegionOptionVmQuery { Id = id }, cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EngageRegionVm>> GetVm([FromRoute] EngageRegionVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEngageRegionCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateEngageRegionCommand command)
    {
        return Ok(await Mediator.Send(command));
    }


    [HttpDelete("delete/{id}")]
    public override async Task<IActionResult> Delete([FromRoute] int id)
    {

        var command = new DeleteOptionCommand
        {
            Option = OptionDesc.ENGAGEREGIONS,
            Id = id,

        };

        return Ok(await Mediator.Send(command));
    }
}
