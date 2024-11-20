using Engage.Application.Services.StoreAssetFiles.Commands;
using Engage.Application.Services.StoreAssetFiles.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("store")]
public partial class StoreAssetFileController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<StoreAssetFileDto>>> List([FromQuery] StoreAssetFileListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<StoreAssetFileDto>(entities));
    }

    [HttpGet("parentId/{storeAssetId}")]
    public async Task<ActionResult<ListResult<StoreAssetFileDto>>> DtoListByParent([FromRoute] StoreAssetFileListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<StoreAssetFileDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<StoreAssetFileOption>>> Options([FromQuery] StoreAssetFileOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StoreAssetFileVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new StoreAssetFileVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(StoreAssetFileInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.StoreAssetFileId));
    }

    //[HttpPut("migrate")]
    //public async Task<IActionResult> MigrateFiles(StoreAssetFileMigrateBlobCommand command, CancellationToken cancellationToken)
    //{
    //    return Ok(await Mediator.Send(command, cancellationToken));
    //}

    [HttpPut]
    public async Task<IActionResult> Update(StoreAssetFileUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.StoreAssetFileId));
    }

    [HttpPut("file/{storeAssetId}")]
    public async Task<IActionResult> FileUpload([FromForm] StoreAssetFileFileUploadCommand command, [FromRoute] int storeAssetId, CancellationToken cancellationToken)
    {
        command.StoreAssetId = storeAssetId;
        var file = await Mediator.Send(command, cancellationToken);

        return file == null ? NotFound() : Ok(new OperationStatus(command.Id, file));
    }

    [HttpDelete("file/{storeAssetId}")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string fileType, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new StoreAssetFileFileDeleteCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(fileType) ? HttpUtility.UrlDecode(fileType) : null
        }, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(id));
    }

    [HttpDelete()]
    public async Task<IActionResult> Delete(StoreAssetFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(command.Id));
    }

}
