using Engage.Application.Services.AssetImages.Commands;
using Engage.Application.Services.AssetImages.Queries;
using Engage.Application.Services.EntityBlobs.Models;
using Engage.Application.Services.Mobile.StoreAsset.Queries;
using Engage.Application.Services.StoreAssets.Commands;
using Engage.Application.Services.StoreAssets.Models;
using Engage.Application.Services.StoreAssets.Queries;
using Engage.Application.Services.StoreAssetSubTypes.Queries;

namespace Engage.WebApi.Controllers.Mobile;


public class StoreAssetController : BaseMobileController
{
    [HttpGet("storeId/{storeid}")]
    public async Task<ActionResult<ListResult<StoreAssetDto>>> GetAll([FromQuery] StoreAssetsQuery query, [FromRoute] int storeId)
    {
        query.StoreId = storeId;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("groupedAssets/storeId/{storeid}")]
    public async Task<ActionResult<ListResult<StoreAssetDto>>> GetGroupedAssets([FromQuery] GetMobileStoreAssetTypeGroupQuery query, [FromRoute] int storeId)
    {
        query.StoreId = storeId;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("GetAssetSubType")]
    public async Task<ActionResult<ListResult<StoreAssetDto>>> GetAssetSubType([FromQuery] GetMobileStoreAssetSubTypeQuery query, [FromQuery] int assetID, [FromQuery] int storeId)
    {
        query.AssetId = assetID;
        query.StoreId = storeId;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option/storeId/{storeid}")]
    public async Task<ActionResult<ListResult<OptionDto>>> GetAll([FromRoute] StoreAssetsOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("offline/options")]
    public async Task<ActionResult<ListResult<OptionDto>>> GetAllOffline([FromQuery] StoreAssetsOptionsOfflineQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<StoreAssetVm>> GetVm([FromRoute] StoreAssetVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(StoreAssetCreateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(StoreAssetUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpGet("storeassetid/{storeassetid}")]
    public async Task<ActionResult<ListResult<EntityBlobDto>>> GetByAsset([FromRoute] StoreAssetBlobsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("storeassetsubtype/options")]
    public async Task<ActionResult<IEnumerable<StoreAssetSubTypeOption>>> OptionList([FromQuery] StoreAssetSubTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }


    [HttpPut("upload/storeassetid/{storeassetid}")]
    public async Task<IActionResult> Upload([FromForm] int id, IFormCollection form)
    {
        return Ok(await Mediator.Send(new StoreAssetBlobUploadCommand
        {
            StoreAssetId = id,
            File = form.Files[0]
        }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await Mediator.Send(new StoreAssetBlobDeleteCommand
        {
            Id = id
        }));
    }
}
