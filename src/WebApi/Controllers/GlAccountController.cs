using Engage.Application.Services.GLAccounts.Commands;
using Engage.Application.Services.GLAccounts.Models;
using Engage.Application.Services.GLAccounts.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("generalledger")]
public class GLAccountController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<GLAccountDto>>> GetAll([FromRoute] GetGLAccountListQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GLAccountDto>> GetById([FromRoute] GetGLAccountQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateGLAccountCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateGLAccountCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
