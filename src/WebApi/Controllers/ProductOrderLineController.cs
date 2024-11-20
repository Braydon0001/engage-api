using Engage.Application.Services.ProductOrderLines.Commands;
using Engage.Application.Services.ProductOrderLines.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProductOrderLineController : BaseController
{
    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProductOrderLineOption>>> Options([FromQuery] ProductOrderLineOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductOrderLineVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProductOrderLineVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpGet("order/{id}")]
    public async Task<ActionResult<ListResult<ProductOrderLineDto>>> GetByOrder([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entities = await Mediator.Send(new ProductOrderLineListQuery { ProductOrderId = id }, cancellationToken);

        return Ok(entities);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProductOrderLineInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProductOrderLineId));
    }

    [HttpPost("lines")]
    public async Task<IActionResult> InsertLines(ProductOrderLinesInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus
        {
            Status = true,
            RecordsAffected = entity.Count,
            ReturnObject = entity.Select(e => e.ProductOrderLineId).ToList()
        });
    }

    [HttpPost("delete")]
    public async Task<ActionResult> DeleteOrderLine(ProductOrderLineDeleteCommand query, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(query, cancellationToken);
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductOrderLineUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProductOrderLineId));
    }

    [HttpPut("quantity")]
    public async Task<IActionResult> UpdateQuantity(ProductOrderLineQuantityUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPut("note")]
    public async Task<IActionResult> UpdateNote(ProductOrderLineNoteUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);
        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProductOrderLineId));
    }

    [HttpPut("file")]
    public async Task<IActionResult> FileUpload([FromForm] ProductOrderLineFileUploadCommand command, CancellationToken cancellationToken)
    {
        var file = await Mediator.Send(command, cancellationToken);

        return file == null ? NotFound() : Ok(new OperationStatus(command.Id, file));
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new ProductOrderLineFileDeleteCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(id));
    }


}
