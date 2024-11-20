// auto-generated
using Engage.Application.Services.OrderTemplateProducts.Commands;
using Engage.Application.Services.OrderTemplateProducts.Queries;

namespace Engage.WebApi.Controllers;

public partial class OrderTemplateProductController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<OrderTemplateProductDto>>> DtoList([FromQuery] OrderTemplateProductListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<OrderTemplateProductDto>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderTemplateProductVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new OrderTemplateProductVmQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(OrderTemplateProductInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.OrderTemplateProductId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(OrderTemplateProductUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.OrderTemplateProductId));
    }

    [HttpPut("price")]
    public async Task<IActionResult> PriceUpdate(OrderTemplateProductPriceUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("promotionprice")]
    public async Task<IActionResult> PromotionPriceUpdate(OrderTemplateProductPromotionPriceUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("recommendedprice")]
    public async Task<IActionResult> RecommendedPriceUpdate(OrderTemplateProductRecommendedPriceUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("grossprofitpercent")]
    public async Task<IActionResult> GrossProfitPercentUpdate(OrderTemplateProductGrossProfitPercentUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("suffix")]
    public async Task<IActionResult> SuffixUpdate(OrderTemplateProductSuffixUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete([FromRoute] int id)
    {
        var entity = await Mediator.Send(new OrderTemplateProductDeleteCommand { Id = id });

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.OrderTemplateProductId));
    }

    [HttpPut("file")]
    public async Task<ActionResult<OperationStatus>> UploadFile([FromForm] OrderTemplateProductUploadFileCommand command)
    {
        var result = await Mediator.Send(command);

        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new OrderTemplateProductDeleteFileCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result.Status == false ? NotFound() : Ok(new OperationStatus(id));
    }
}