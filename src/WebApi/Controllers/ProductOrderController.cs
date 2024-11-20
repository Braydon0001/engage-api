using Engage.Application.Services.ProductOrders.Commands;
using Engage.Application.Services.ProductOrders.Queries;
using Engage.WebApi.utils;

namespace Engage.WebApi.Controllers;

public partial class ProductOrderController : BaseController
{
    [HttpPost("page")]
    public async Task<ActionResult<PaginatedListResult<ProductOrderDto>>> PaginatedQuery(ProductOrderPaginatedQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);
        return Ok(new ListResult<ProductOrderDto>(entities));
    }

    [HttpPost("process/page")]
    public async Task<ActionResult<ProcessProductOrderDto>> ProcessPaginatedQuery(ProductOrderPaginatedQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);
        return Ok(new ProcessProductOrderDto
        {
            Count = entities.Count,
            Data = entities,
            PageNumber = query.StartRow,
            PageSize = query.PageSize,
        });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductOrderVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProductOrderVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpGet("summary/{id}")]
    public async Task<ActionResult<ProductOrderSummaryDto>> SummaryVm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProductOrderSummaryQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProductOrderInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProductOrderId));
    }

    [HttpPost("report")]
    public async Task<IActionResult> GenerateReport(ProductOrderReportQuery query, CancellationToken cancellationToken)
    {
        var data = await Mediator.Send(query, cancellationToken);
        var file = ExcelFileGeneratorUtil.GeneratePurchaseOrderStream(data);

        var contentType = "application/octet-stream";
        var fileName = data.First().FileName;

        return File(file, contentType, fileName);
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductOrderUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProductOrderId));
    }

    [HttpPut("orderDate")]
    public async Task<IActionResult> UpdateOrderDate(ProductOrderOrderDateUpadateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);
        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProductOrderId));
    }

    [HttpPut("productWarehouseId")]
    public async Task<IActionResult> UpdateProductWarehouse(ProductOrderWarehouseUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);
        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProductOrderId));
    }

    [HttpPut("status")]
    public async Task<IActionResult> UpdateStatus(ProductOrderStatusUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);
        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProductOrderId));
    }

    [HttpPut("file")]
    public async Task<IActionResult> FileUpload([FromForm] ProductOrderFileUploadCommand command, CancellationToken cancellationToken)
    {
        var file = await Mediator.Send(command, cancellationToken);

        return file == null ? NotFound() : Ok(new OperationStatus(command.Id, file));
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new ProductOrderFileDeleteCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(id));
    }

}
