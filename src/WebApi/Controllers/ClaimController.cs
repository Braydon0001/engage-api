using Engage.Application.Services.Claims.Commands;
using Engage.Application.Services.Claims.Models;
using Engage.Application.Services.Claims.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("claim")]
public class ClaimController : BaseController
{
    [HttpPost("page")]
    public async Task<ActionResult<ListResult<ClaimSubTotalDto>>> PaginatedQuery(ClaimPaginatedQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("claimstatusid/{claimstatusid}/claimsupplierstatusid/{claimsupplierstatusid}/claimclassificationids/{claimclassificationids}/engageregionids/{engageregionids}/claimperiodid/{claimperiodid}/employeedivisionid/{employeedivisionid}/")]
    public async Task<ActionResult<PaginatedListResult<ClaimSubTotalDto>>> GetByStatus([FromRoute] int? claimStatusId, [FromRoute] int? claimSupplierStatusId, [FromRoute] string claimClassificationIds, string engageRegionIds, [FromRoute] int? claimPeriodId, [FromRoute] int? employeeDivisionId)
    {
        List<int> classificationIds = claimClassificationIds.Split(',').Select(int.Parse).ToList();
        List<int> regionIds = engageRegionIds.Split(',').Select(int.Parse).ToList();

        return Ok(await Mediator.Send(new GetProcessClaimsListQuery
        {
            ClaimStatusId = claimStatusId,
            ClaimSupplierStatusId = claimSupplierStatusId,
            ClaimClassificationIds = classificationIds,
            EngageRegionIds = regionIds,
            ClaimPeriodId = claimPeriodId,
            EmployeeDivisionId = employeeDivisionId,
        }));
    }

    [HttpPost("processclaimslist")]
    public async Task<IActionResult> GetAll([FromBody] GetProcessClaimsListQuery command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpGet("claimsupplierstatusid/{claimsupplierstatusid}")]
    public async Task<ActionResult<PaginatedListResult<ClaimSubTotalDto>>> GetBySupplierStatus([FromRoute] int? claimSupplierStatusId)
    {
        return Ok(await Mediator.Send(new ClaimsQuery
        {
            ClaimStatusId = (int?)ClaimStatusId.Approved,
            ClaimSupplierStatusId = claimSupplierStatusId,
        }));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClaimVm>> GetVm([FromRoute] ClaimVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option/suppliers")]
    public async Task<ActionResult<List<ClaimSupplierOptionDto>>> ClaimSupplierOptions([FromQuery] ClaimSupplierOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option/stores")]
    public async Task<ActionResult<List<ClaimStoreOptionDto>>> ClaimStoreOptions([FromQuery] ClaimStoreOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option/claimaccountmanagers/supplierId/{supplierId}")]
    public async Task<ActionResult<List<OptionDto>>> ClaimAccountManagerOptions([FromRoute] ClaimAccountManagerOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option/claimmanagers/storeId/{storeId}")]
    public async Task<ActionResult<List<OptionDto>>> ClaimManagerOptionsQuery([FromRoute] ClaimManagerOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option")]
    public async Task<ActionResult<List<OptionDto>>> GetByType([FromRoute] ClaimOptionsQuery query, [FromQuery] string search)
    {
        query.Search = search;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option/voucherId/{voucherid}/")]
    public async Task<ActionResult<List<OptionDto>>> GetOptionsByVoucher([FromQuery] VoucherClaimOptionsQuery query, [FromRoute] int voucherId)
    {
        query.VoucherId = voucherId;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option/project")]
    public async Task<ActionResult<List<OptionDto>>> GetOptionsByProject([FromQuery] ProjectClaimOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateClaimCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateClaimCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("claimdate")]
    public async Task<IActionResult> UpdateClaimDate(UpdateClaimDateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("claimnumber")]
    public async Task<IActionResult> UpdateClaimNumber(UpdateClaimNumberCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("claimreference")]
    public async Task<IActionResult> UpdateClaimReference(UpdateClaimReferenceCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("claimstatus")]
    public async Task<IActionResult> UpdateClaimStatus(UpdateClaimStatusCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("supplierid")]
    public async Task<IActionResult> UpdateClaimSupplierId(UpdateClaimSupplierIdCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("storeid")]
    public async Task<IActionResult> UpdateClaimStoreId(UpdateClaimStoreIdCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("ispaystore")]
    public async Task<IActionResult> UpdateClaimIsPayStore(UpdateClaimIsPayStoreCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("isclaimfromsupplier")]
    public async Task<IActionResult> UpdateClaimIsClaimFromSupplier(UpdateClaimIsClaimFromSupplierCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("claimaccountmanagerid")]
    public async Task<IActionResult> UpdateClaimAccountManagerId(UpdateClaimAccountManagerIdCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("claimfloatid")]
    public async Task<IActionResult> UpdateClaimFloatId(UpdateClaimFloatIdCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("employeedivisionid")]
    public async Task<IActionResult> UpdateClaimDivisionId(UpdateClaimDivisionIdCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("claimmanagerid")]
    public async Task<IActionResult> UpdateClaimManagerId(UpdateClaimManagerIdCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("batch/claimstatus")]
    public async Task<IActionResult> BatchUpdateClaimStatus(BatchUpdateClaimStatusCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("claimsupplierstatus")]
    public async Task<IActionResult> UpdateClaimSupplierStatus(UpdateClaimSupplierStatusCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("batch/claimsupplierstatus")]
    public async Task<IActionResult> BatchUpdateClaimSupplierStatus(BatchUpdateClaimSupplierStatusCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("delete")]
    public async Task<IActionResult> Delete(DeleteClaimCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
