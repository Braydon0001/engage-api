using Engage.Application.Services.EngageRegionClaimManagers.Commands;
using Engage.Application.Services.EngageRegionClaimManagers.Models;
using Engage.Application.Services.EngageRegionClaimManagers.Queries;

namespace Engage.WebApi.Controllers;

public record EngageRegionClaimManagerParam(int EngageRegionId, int UserId);

public class EngageRegionClaimManagerController : BaseController
{
    [HttpGet("EngageRegionId/{EngageRegionId}")]
    public async Task<ActionResult<ListResult<EngageRegionClaimManagerDto>>> GetByEngageRegion([FromRoute] EngageRegionClaimManagersQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("ClaimManagerId/{ClaimManagerId}")]
    public async Task<ActionResult<ListResult<EngageRegionClaimManagerDto>>> GetByClaimManager([FromRoute] EngageRegionClaimManagersQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost()]
    public async Task<IActionResult> Create(CreateEngageRegionClaimManagersCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete()]
    public async Task<IActionResult> Delete(EngageRegionClaimManagerDeleteCommand command)
    {
        var entity = await Mediator.Send(command);

        return entity == null ? NotFound() : Ok(new OperationStatus
        {
            Status = true,
            RecordsAffected = 1,
            OperationId = new
            {
                command.EngageRegionId,
                command.UserId
            }
        });
    }

}