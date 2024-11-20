using Engage.Application.Services.ClaimSkus.Commands;
using Engage.Application.Services.ClaimSkus.Models;
using Engage.Application.Services.ClaimSkus.Queries;
using Engage.Application.Services.Products.Models;
using Engage.Application.Services.Products.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("claim")]
public class ClaimSkuController : BaseController
{
    [HttpGet("claim/{claimid}")]
    public async Task<ActionResult<ListResult<ClaimSkuDto>>> GetAllByClaim([FromRoute] GetClaimSkusQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClaimSkuVm>> GetVm([FromRoute] GetClaimSkuVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("product/distributioncenter/{distributionCenterId}/supplier/{supplierId}")]
    public async Task<ActionResult<List<ProductOptionDto>>> GetProducts([FromQuery] string search, [FromRoute] int distributionCenterId, [FromRoute] int supplierId)
    {
        return Ok(await Mediator.Send(new ClaimProductsQuery
        {
            Search = search,
            DistributionCenterId = distributionCenterId,
            SupplierId = supplierId,
        }));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateClaimSkuCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("skus")]
    public async Task<IActionResult> CreateSkus(CreateClaimSkusCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("delete")]
    public async Task<IActionResult> Delete(DeleteClaimSkuCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateClaimSkuCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("claimquantitytype")]
    public async Task<IActionResult> UpdateClaimSkuQuantityType(UpdateClaimSkuQuantityTypeCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("quantity")]
    public async Task<IActionResult> UpdateClaimSkuQuantity(UpdateClaimSkuQuantityCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("amount")]
    public async Task<IActionResult> UpdateClaimSkuAmount(UpdateClaimSkuAmountCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("note")]
    public async Task<IActionResult> UpdateClaimSkuNote(UpdateClaimSkuNoteCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
