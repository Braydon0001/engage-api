using Engage.Application.Services.StoreAssetOwners.Queries;

namespace Engage.WebApi.Controllers;

public class StoreAssetOwnerController : BaseController
{
    [HttpGet("options/assettype")]
    public async Task<ActionResult<ListResult<StoreAssetOwnerOption>>> GetByAssetTypeOptions([FromQuery] StoreAssetOwnerOptionByTypeQuery query, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(query, cancellationToken));
    }
}
