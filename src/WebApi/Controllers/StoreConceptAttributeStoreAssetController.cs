using Engage.Application.Services.StoreConceptAttributeStoreAssets.Commands;
using Engage.Application.Services.StoreConceptAttributeStoreAssets.Models;
using Engage.Application.Services.StoreConceptAttributeStoreAssets.Queries;

namespace Engage.WebApi.Controllers;

public record BatchAssignAssetParam(int StoreConceptAttributeId, List<int> StoreAssetIds);

public class StoreConceptAttributeStoreAssetController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<StoreConceptAttributeStoreAssetDto>>> Get([FromQuery] StoreConceptAttributeStoreAssetQuery query)
    {
        var result = await Mediator.Send(query);

        return result == null ? NotFound() : Ok(result);
    }

    [HttpGet("detail")]
    public async Task<ActionResult<StoreConceptAttributeStoreAssetVm>> GetVmQuery([FromQuery] StoreConceptAttributeStoreAssetVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(StoreConceptAttributeStoreAssetCreateCommand command, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(command, cancellationToken));
    }

    [HttpPost("batchassignasset")]
    public async Task<IActionResult> Create(BatchAssignAssetParam batchAssignAssetParam)
    {
        return Ok(await Mediator.Send(new BatchAssignCommand(AssignDesc.STORE_CONCEPT_ATTRIBUTE_ASSET, batchAssignAssetParam.StoreConceptAttributeId, batchAssignAssetParam.StoreAssetIds)));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(StoreConceptAttributeStoreAssetDeleteCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        return result == null ? NotFound() : Ok(result);
    }
}
