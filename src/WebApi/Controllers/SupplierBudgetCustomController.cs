using Engage.Application.Services.SupplierBudgets.Queries;

namespace Engage.WebApi.Controllers;

public partial class SupplierBudgetController
{
    [HttpGet("suppliercontract/{supplierContractId}/version/{supplierBudgetVersionId}/type/{supplierBudgetTypeId}")]
    public async Task<ActionResult<ListResult<SupplierBudgetDto>>> DtoListBySupplierContract([FromRoute] SupplierBudgetListBySupplierContractQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SupplierBudgetDto>(entities));
    }
}
