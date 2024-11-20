using Engage.Application.Services.Products.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProductController : BaseController
{
    [HttpGet("option")]
    public async Task<ActionResult<List<OptionDto>>> GetOptions([FromRoute] ProductOptionQuery query, string search, int productSupplierId, int productWarehouseOutId)
    {
        query.Search = search;
        query.ProductSupplierId = productSupplierId;
        query.ProductWarehouseOutId = productWarehouseOutId;
        return Ok(await Mediator.Send(query));
    }

    [HttpPost("tree")]
    public async Task<ActionResult<ListResult<ProductsTreeDto>>> TreeQuery(ProductTreeQuery query, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(query, cancellationToken));
    }
}
