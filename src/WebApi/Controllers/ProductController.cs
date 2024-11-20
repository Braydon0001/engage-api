// auto-generated
using Engage.Application.Services.Products.Commands;
using Engage.Application.Services.Products.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProductController : BaseController
{
    [HttpPost("page")]
    public async Task<ActionResult<PaginatedListResult<ProductDto>>> PaginatedQuery(ProductPaginatedQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("options/page")]
    public async Task<ActionResult<IEnumerable<ProductOption>>> PaginatedOptionQuery(ProductPaginatedOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("option/price/{id}")]
    public async Task<ActionResult<ProductWithPriceDto>> ProductPriceOption([FromRoute] ProductWithPriceQuery query, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(query, cancellationToken);
        return Ok(entity);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProductVmQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpGet("productmaster/{productmasterid}")]
    public async Task<ActionResult<ListResult<ProductDto>>> ProductByMasterQuery([FromRoute] ProductByMasterQuery query, CancellationToken cancellationToken)
    {

        return Ok(await Mediator.Send(query, cancellationToken));
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProductInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProductId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProductId));
    }

    [HttpPut("file")]
    public async Task<IActionResult> FileUpload([FromForm] ProductFileUploadCommand command, CancellationToken cancellationToken)
    {
        var file = await Mediator.Send(command, cancellationToken);

        return file == null ? NotFound() : Ok(new OperationStatus(command.Id, file));
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new ProductFileDeleteCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result == false ? NotFound() : Ok(new OperationStatus(id));
    }

}