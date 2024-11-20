using Engage.Application.Services.StorePOS.Commands;
using Engage.Application.Services.StorePOS.Models;
using Engage.Application.Services.StorePOS.Queries;

namespace Engage.WebApi.Controllers;

public class StorePOSController : BaseController
{
    [HttpGet("store/{storeId}")]
    public async Task<ActionResult<ListResult<StorePOSDto>>> GetAll([FromRoute] GetStorePOSListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("national/store/{storeId}")]
    public async Task<ActionResult<ListResult<StorePOSDto>>> GetNational([FromRoute] GetStorePOSListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("freashline/store/{storeId}")]
    public async Task<ActionResult<ListResult<StorePOSDto>>> GetFreshline([FromRoute] GetStorePOSListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StorePOSVm>> GetVm([FromRoute] GetStorePOSVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateStorePOSCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateStorePOSCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
