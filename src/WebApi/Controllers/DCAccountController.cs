using Engage.Application.Services.DCAccounts.Commands;
using Engage.Application.Services.DCAccounts.Models;
using Engage.Application.Services.DCAccounts.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("store")]
public class DCAccountController : BaseController
{
    [HttpGet()]
    public async Task<ActionResult<ListResult<DCAccountDto>>> GetAll([FromQuery] DCAccountsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
    
    [HttpGet("storeId/{storeId}")]
    public async Task<ActionResult<ListResult<DCAccountDto>>> GetByStore([FromRoute] DCAccountsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DCAccountVm>> GetVm([FromRoute] DCAccountVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateDCAccountCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateDCAccountCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
