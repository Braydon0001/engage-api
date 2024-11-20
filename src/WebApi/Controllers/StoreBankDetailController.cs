using Engage.Application.Services.StoreBankDetails.Commands;
using Engage.Application.Services.StoreBankDetails.Models;
using Engage.Application.Services.StoreBankDetails.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("store")]
public class StoreBankDetailController : BaseController
{
    [HttpGet("storeId/{storeId}")]

    public async Task<ActionResult<ListResult<StoreBankDetailDto>>> GetByStore([FromRoute] StoreBankDetailsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StoreBankDetailVm>> GetVm([FromRoute] StoreBankDetailVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateStoreBankDetailCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateStoreBankDetailCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
