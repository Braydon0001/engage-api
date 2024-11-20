using Engage.Application.Services.Claims.Commands;
using Engage.Application.Services.Claims.Models;
using Engage.Application.Services.Claims.Queries;

namespace Engage.WebApi.Controllers.Mobile
{
    public class ClaimController : BaseMobileController
    {

        [HttpPost]
        public async Task<IActionResult> Create(CreateClaimCommand command)
        {
            return Ok(await Mediator.Send(command));
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

        [HttpGet("option/claimmanagers/storeId/{storeId}")]
        public async Task<ActionResult<List<OptionDto>>> ClaimManagerOptionsQuery([FromRoute] ClaimManagerOptionsQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("option/claimaccountmanagers/supplierId/{supplierId}")]
        public async Task<ActionResult<List<OptionDto>>> ClaimAccountManagerOptions([FromRoute] ClaimAccountManagerOptionsQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }



}
