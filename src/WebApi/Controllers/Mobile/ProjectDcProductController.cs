using Engage.Application.Services.DCProducts.Queries;

namespace Engage.WebApi.Controllers.Mobile;

public partial class ProjectDcProductController : BaseMobileController
{
    [HttpGet("options/store")]
    public async Task<ActionResult<List<OptionDto>>> GetDCProductOptionsByStore([FromQuery] DCProductOptionsByStoreQuery query)
    {
        return Ok(await Mediator.Send(query));
    }


    [HttpGet("offline/options")]
    public async Task<ActionResult<List<OptionDto>>> GetDCProductOptionsOffline([FromQuery] DCProductOptionsOfflineQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}
