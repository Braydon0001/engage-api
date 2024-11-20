using Engage.Application.Services.DCProducts.Models;
using Engage.Application.Services.DCProducts.Queries;
using Engage.Application.Services.OrderSkus.Commands;
using Engage.Application.Services.OrderSkus.Models;
using Engage.Application.Services.OrderSkus.Queries;
using Engage.Application.Services.Products.Models;
using Engage.Application.Services.Products.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("order")]
public class OrderSkuController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<OrderSkuListItemDto>>> DtoList([FromRoute] OrderSkuListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("orderid/{orderId?}")]
    public async Task<ActionResult<ListResult<OrderSkuListItemDto>>> DtoListOrderId([FromRoute] int orderId)
    {
        return Ok(await Mediator.Send(new OrderSkuListQuery
        {
            OrderId = orderId
        }));
    }

    [HttpGet("orderids/{orderIds}")]
    public async Task<ActionResult<ListResult<OrderSkuListItemDto>>> DtoListOrderIds([FromRoute] string orderIds)
    {
        return Ok(await Mediator.Send(new OrderSkuListQuery
        {
            OrderIds = orderIds
        }));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderSkuVM>> Vm(int id)
    {
        return Ok(await Mediator.Send(new GetOrderSkuViewModelQuery() { Id = id }));
    }

    [HttpGet("products/{distributioncenterid}")]
    public async Task<ActionResult<List<ProductOptionDto>>> Product([FromQuery] GetQuery query, [FromRoute] int distributionCenterId)
    {
        return Ok(await Mediator.Send(query.Merge(new ProductsQuery
        {
            DistributionCenterId = distributionCenterId,
            IsSupplierProductsOnly = true
        })));
    }

    [HttpGet("products/history")]
    public async Task<ActionResult<List<ProductOptionDto>>> ProductHistory([FromQuery] GetOrderSkuProductsHistoryQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost("products/multiplesubwarehouse")]
    public async Task<ActionResult<ListResult<VariantDCProductDto>>> ProductMultipleSubWarehouse(MultipleDCProductsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Insert
        (CreateOrderSkuCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("skus")]
    public async Task<IActionResult> CreateSkus(CreateOrderSkusCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("skuswithdate")]
    public async Task<IActionResult> CreateSkusWithDate(CreateOrderSkusWithDateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("description")]
    public async Task<IActionResult> CreateDescription(CreateOrderSkuDescriptionCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("template")]
    public async Task<IActionResult> OrderSkuTemplateProductsInsert(OrderSkuTemplateProductsInsertCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(OrderSkuUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("quantitytype")]
    public async Task<IActionResult> QuantityTypeUpdate(OrderSkuQuantityTypeUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("quantity")]
    public async Task<IActionResult> QuantityUpdate(OrderSkuQuantityUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("price")]
    public async Task<IActionResult> PriceUpdate(OrderSkuPriceUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("promotionprice")]
    public async Task<IActionResult> PromotionPriceUpdate(OrderSkuPromotionPriceUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("recommendedprice")]
    public async Task<IActionResult> RecommendedPriceUpdate(OrderSkuRecommendedPriceUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("grossprofitpercent")]
    public async Task<IActionResult> GrossProfitPercentUpdate(OrderSkuGrossProfitPercentUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("suffix")]
    public async Task<IActionResult> SuffixUpdate(OrderSkuSuffixUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("note")]
    public async Task<IActionResult> NoteUpdateateNote(OrderSkuNoteUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("file")]
    public async Task<ActionResult<OperationStatus>> UploadFile([FromForm] OrderSkuUploadFileCommand command)
    {
        var result = await Mediator.Send(command);

        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new OrderSkuDeleteFileCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result.Status == false ? NotFound() : Ok(new OperationStatus(id));
    }
}
