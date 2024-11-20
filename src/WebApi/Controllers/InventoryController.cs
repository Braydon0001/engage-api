using Engage.Application.Services.Inventories.Commands;
using Engage.Application.Services.Inventories.Queries;

namespace Engage.WebApi.Controllers;

public partial class InventoryController : BaseController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<InventoryVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new InventoryVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost("page")]
    public async Task<ActionResult<ListResult<InventoryDto>>> Paginated(InventoryPaginatedQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<InventoryDto>(entities));
    }

    [HttpPost("options/page")]
    public async Task<ActionResult<IEnumerable<InventoryOption>>> PaginatedOption(InventoryPaginatedOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(InventoryInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.InventoryId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(InventoryUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.InventoryId));
    }

    //[HttpDelete("{id}")]
    //public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    //{
    //    if (id <= 0)
    //    {
    //        return BadRequest(BadIdText);
    //    }

    //    var entity = await Mediator.Send(new InventoryDeleteCommand(id), cancellationToken);

    //    return entity == null ? NotFound() : Ok(new OperationStatus(entity.InventoryId));
    //}

    [HttpPut("file")]
    public async Task<IActionResult> FileUpload([FromForm] InventoryFileUploadCommand command, CancellationToken cancellationToken)
    {
        var file = await Mediator.Send(command, cancellationToken);

        return file == null ? NotFound() : Ok(new OperationStatus(command.Id, file));
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new InventoryFileDeleteCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(id));
    }

}
