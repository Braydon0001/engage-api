using Engage.Application.Services.StoreOwners.Queries;

namespace Engage.WebApi.Controllers;

public partial class StoreOwnerController : BaseController
{
    [HttpGet("store/{storeid}")]
    public async Task<ActionResult<ListResult<StoreOwnerDto>>> StoreById([FromRoute] int storeId, CancellationToken cancellationToken)
    {
        if (storeId <= 0)
        {
            return BadRequest(BadIdText);
        }
        var entities = await Mediator.Send(new StoreOwnerByStoreQuery { StoreId = storeId}, cancellationToken);

        return Ok(new ListResult<StoreOwnerDto>(entities));
    }
}
