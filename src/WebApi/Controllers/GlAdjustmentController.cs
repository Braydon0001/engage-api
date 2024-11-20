using Engage.Application.Services.GlAdjustments.Commands;
using Engage.Application.Services.GlAdjustments.Models;
using Engage.Application.Services.GlAdjustments.Queries;
using Engage.Application.Services.GLAdjustments.Models;
using Engage.Application.Services.GLAdjustments.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("generalledger")]
public class GLAdjustmentController : BaseController
{

    [HttpGet]
    public async Task<ActionResult<ListResult<GLAdjustmentDto>>> GetAll([FromRoute] GlAdjustmentsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GLAdjustmentDto>> GetById([FromRoute] GlAdjustmentQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("Year/{YearId}/Period/{PeriodId}")]
    public async Task<ActionResult<ListResult<GlAdjustmentTableDataDto>>> GetAllGLAdjustment([FromRoute] GlAdjustmentTableDataQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateGLAdjustmentCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateGLAdjustmentCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
