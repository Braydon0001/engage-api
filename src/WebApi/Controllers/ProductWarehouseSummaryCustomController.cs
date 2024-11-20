using Engage.Application.Services.ProductWarehouseSummaries.Commands;
using Engage.Application.Services.ProductWarehouseSummaries.Queries;
using Engage.WebApi.utils;

namespace Engage.WebApi.Controllers;

public partial class ProductWarehouseSummaryController : BaseController
{
    [HttpGet("history/productId/{productId}/warehouseId/{productWarehouseId}")]
    public async Task<ActionResult<ProductWarehouseSummaryProductHistoryDto>> GetStockHistory([FromRoute] ProductWarehouseSummaryProductHistoryQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);
        return Ok(entities);
    }

    [HttpPost("tree")]
    public async Task<ActionResult<ListResult<ProductWarehouseSummaryStockOnHandTreeDto>>> GetStockOnHand(ProductWarehouseSummaryStockOnHandTreeQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);
        return Ok(new ListResult<ProductWarehouseSummaryStockOnHandTreeDto>(entities));
    }

    [HttpPost("page")]
    public async Task<ActionResult<ListResult<ProductWarehouseQuantitiesDto>>> PaginatedQuery(ProductWarehouseSummaryPaginatedQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);
        return Ok(entities);
    }

    [HttpPost("report")]
    public async Task<IActionResult> StockReport(ProductWarehouseSummaryStockReportQuery query, CancellationToken cancellationToken)
    {
        var data = await Mediator.Send(query, cancellationToken);
        var file = await ExcelFileGeneratorUtil.GenerateStockOnHandSummary(data);

        var contentType = "application/octet-stream";
        var fileName = $"Stock on Hand report {DateTime.Now.ToShortDateString().Replace('/', '-')}.xlsx";

        return File(file, contentType, fileName);
    }

    [AllowAnonymous]
    [HttpPost("closingbalance")]
    public async Task<IActionResult> GenerateClosingBalance()
    {
        var entity = await Mediator.Send(new ProductWarehouseSummaryInsertMonthlyStockCommand { });
        return Ok(entity);
    }
}
