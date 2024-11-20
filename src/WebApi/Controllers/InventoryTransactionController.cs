// auto-generated
using Engage.Application.Services.InventoryTransactions.Commands;
using Engage.Application.Services.InventoryTransactions.Queries;
namespace Engage.WebApi.Controllers;

public partial class InventoryTransactionController : BaseController
{
    [HttpPost("page")]
    public async Task<ActionResult<ListResult<InventoryTransactionDto>>> PaginatedList([FromQuery] InventoryTransactionPaginatedListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<InventoryTransactionDto>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InventoryTransactionVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new InventoryTransactionVmQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(InventoryTransactionInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.InventoryTransactionId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(InventoryTransactionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.InventoryTransactionId));
    }

    [HttpPut("file")]
    public async Task<IActionResult> FileUpload([FromForm] InventoryTransactionFileUploadCommand command, CancellationToken cancellationToken)
    {
        var file = await Mediator.Send(command, cancellationToken);

        return file == null ? NotFound() : Ok(new OperationStatus(command.Id, file));
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new InventoryTransactionFileDeleteCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(id));
    }
}