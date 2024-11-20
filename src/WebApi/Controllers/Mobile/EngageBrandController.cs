using Engage.Application.Services.EngageBrands.Queries;

namespace Engage.WebApi.Controllers.Mobile;

public partial class EngageBrandController : BaseMobileController
{
    [HttpGet("options/supplier")]
    public async Task<ActionResult<List<EngageBrandOption>>> OptionListBySupplier([FromQuery] EngageBrandBySupplierQuery query, CancellationToken cancellationToken)
    {
        var options = await Mediator.Send(query, cancellationToken);

        return Ok(options);
    }

    [HttpGet("offline/options/supplier")]
    public async Task<ActionResult<List<EngageBrandOption>>> OfflineOptionListBySupplier([FromQuery] EngageBrandBySupplierOfflineQuery query, CancellationToken cancellationToken)
    {
        var options = await Mediator.Send(query, cancellationToken);

        return Ok(options);
    }
}
