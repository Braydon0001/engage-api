using Engage.Application.Services.SubContractorBrands.Commands;
using Engage.Application.Services.SubContractorBrands.Queries;
using Engage.Application.Services.SupplierBudgets.Queries;

namespace Engage.WebApi.Controllers;

public partial class SubContractorBrandController : BaseController
{
    [HttpGet("supplierId/{id}")]
    public async Task<ActionResult<ListResult<SubContractorBrandDto>>> DtoList([FromRoute] SubContractorBrandListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SubContractorBrandDto>(entities));
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SubContractorBrandInsertCommand command, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(command, cancellationToken));
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(SubContractorBrandDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }
}