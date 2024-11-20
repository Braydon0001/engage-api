using Engage.Application.Services.SupplierProducts.Models;
using Engage.Application.Services.SupplierProducts.Queries;

namespace Engage.WebApi.Controllers;

public record SupplierProductParam(int SupplierId, int EngageMasterProductId);

public class SupplierProductController : BaseController
{
    [HttpGet("supplierId/{supplierId}")]
    public async Task<ActionResult<ListResult<SupplierProductDto>>> GetBySupplier([FromRoute] SupplierProductsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("engageMasterProductId/{engageMasterProductId}")]
    public async Task<ActionResult<ListResult<SupplierProductDto>>> GetByProduct([FromRoute] SupplierProductsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost()]
    public async Task<IActionResult> Create(SupplierProductParam supplierProduct)
    {
        return Ok(await Mediator.Send(new AssignCommand(AssignDesc.PRODUCT_SUPPLIER, supplierProduct.SupplierId, supplierProduct.EngageMasterProductId)));
    }

    [HttpPost("delete")]
    public async Task<IActionResult> Delete(SupplierProductParam supplierProduct)
    {
        return Ok(await Mediator.Send(new UnassignCommand(AssignDesc.PRODUCT_SUPPLIER, supplierProduct.SupplierId, supplierProduct.EngageMasterProductId)));
    }
}