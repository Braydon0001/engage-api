using Engage.Application.Services.ClaimBlobs.Commands;
using Engage.Application.Services.ClaimBlobs.Queries;
using Engage.Application.Services.EntityBlobs.Models;

namespace Engage.WebApi.Controllers;

[Authorize("claim")]
public class ClaimBlobController : BaseController
{
    [HttpGet("claimId/{claimid}")]
    public async Task<ActionResult<ListResult<EntityBlobDto>>> GetByClaim([FromRoute] ClaimBlobsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost("upload/claimId/{claimId}")]
    public async Task<IActionResult> Upload([FromRoute] int claimId, IFormCollection form)
    {
        return Ok(await Mediator.Send(new UploadClaimBlobCommand
        {
            ClaimId = claimId,
            File = form.Files[0]
        }));
    }
}
