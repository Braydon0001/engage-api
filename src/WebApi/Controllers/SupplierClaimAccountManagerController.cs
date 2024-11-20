using Engage.Application.Services.SupplierClaimAccountManagers.Commands;
using Engage.Application.Services.SupplierClaimAccountManagers.Models;
using Engage.Application.Services.SupplierClaimAccountManagers.Queries;

namespace Engage.WebApi.Controllers;

public record SupplierUser(int SupplierId, int UserId);

public class SupplierClaimAccountManagerController : BaseController
{
    [HttpGet("supplierId/{supplierId}")]
    public async Task<ActionResult<ListResult<SupplierClaimAccountManagerDto>>> GetBySupplier([FromRoute] SupplierClaimAccountManagersQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("ClaimAccountManagerId/{ClaimAccountManagerId}")]
    public async Task<ActionResult<ListResult<SupplierClaimAccountManagerDto>>> GetByClaimAccountManager([FromRoute] SupplierClaimAccountManagersQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost()]
    public async Task<IActionResult> Create(CreateSupplierClaimAccountManagersCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("delete")]
    public async Task<IActionResult> Delete(SupplierUser supplierUser)
    {
        return Ok(await Mediator.Send(new UnassignCommand(AssignDesc.CLAIM_ACCOUNT_MANAGER_SUPPLIER, supplierUser.SupplierId, supplierUser.UserId)));
    }
}