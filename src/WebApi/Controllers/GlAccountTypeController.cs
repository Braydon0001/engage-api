using Engage.Application.Services.GlAccountTypes.Commands;
using Engage.Application.Services.GlAccountTypes.Models;
using Engage.Application.Services.GlAccountTypes.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("generalledger")]
public class GLAccountTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<GlAccountTypeDto>>> GetAll([FromRoute] GlAccountTypesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GlAccountTypeDto>> GetById([FromRoute] GlAccountTypeVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateGLAccountTypeCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateGLAccountTypeCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
