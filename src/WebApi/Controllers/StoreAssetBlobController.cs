using Engage.Application.Services.AssetImages.Commands;
using Engage.Application.Services.AssetImages.Queries;
using Engage.Application.Services.EntityBlobs.Models;

namespace Engage.WebApi.Controllers;

[Authorize("store")]
public class StoreAssetBlobController : BaseController
{
    [HttpGet("storeassetid/{storeassetid}")]
    public async Task<ActionResult<ListResult<EntityBlobDto>>> GetByAsset([FromRoute] StoreAssetBlobsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost("upload/storeassetid/{storeassetid}")]
    public async Task<IActionResult> Upload([FromRoute] int storeAssetId, IFormCollection form)
    {
        return Ok(await Mediator.Send(new StoreAssetBlobUploadCommand
        {
            StoreAssetId = storeAssetId,
            File = form.Files[0]
        }));
    }

    [AllowAnonymous]
    [HttpPost("bulkfix")]
    public async Task<IActionResult> BulkFix()
    {
        return Ok(await Mediator.Send(new StoreAssetBlobFixCommand { }));
    }

    [HttpDelete("{id}")]
    public async override Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await Mediator.Send(new StoreAssetBlobDeleteCommand
        {
            Id = id
        }));
    }
}
