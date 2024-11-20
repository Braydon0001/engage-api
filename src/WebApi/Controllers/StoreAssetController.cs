using Engage.Application.Services.StoreAssets.Commands;
using Engage.Application.Services.StoreAssets.Models;
using Engage.Application.Services.StoreAssets.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("store")]
public class StoreAssetController : BaseController
{
    [HttpGet("storeId/{storeid}")]
    public async Task<ActionResult<ListResult<StoreAssetDto>>> GetAll([FromQuery] StoreAssetsQuery query, [FromRoute] int storeId)
    {
        query.StoreId = storeId;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option/storeId/{storeid}")]
    public async Task<ActionResult<ListResult<OptionDto>>> GetAll([FromRoute] StoreAssetsOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option/project")]
    public async Task<ActionResult<ListResult<OptionDto>>> GetOptionsByProject([FromQuery] StoreAssetOptionsByProjectQuery query)
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

    [HttpPut("file")]
    public async Task<IActionResult> FileUpload([FromForm] StoreAssetFileUploadCommand command, CancellationToken cancellationToken)
    {
        var file = await Mediator.Send(command, cancellationToken);

        return file == null ? NotFound() : Ok(new OperationStatus(command.Id, file));
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new StoreAssetFileDeleteCommand
        {
            Id = id,
            FileName = fileName,
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(id));
    }
}
