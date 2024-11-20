using Engage.Application.Services.ClaimTypes.Models;
using Engage.Application.Services.ClaimTypes.Queries;

namespace Engage.WebApi.Controllers.Mobile;

public class ClaimTypeController : BaseMobileController
{
    [HttpGet("option/claimclassificationid/{claimclassificationid}")]
    public async Task<ActionResult<ClaimTypeVm>> GetOptionsByClaimClassificationId([FromRoute] int? claimclassificationid)
    {
        return Ok(await Mediator.Send(new ClaimTypeOptionsQuery
        {
            ClaimClassificationId = claimclassificationid,
        }));
    }


}
