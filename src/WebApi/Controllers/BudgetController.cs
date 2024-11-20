using Engage.Application.Services.Budgets.Commands;
using Engage.Application.Services.Budgets.Models;
using Engage.Application.Services.Budgets.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("budget")]
public class BudgetController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<BudgetDto>>> GetAll([FromRoute] BudgetsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BudgetDto>> GetById([FromRoute] BudgetQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("engageregion")]
    public async Task<ActionResult<EngageRegionBudgetsVm>> GetEngageRegionBudgets([FromQuery] EngageRegionBudgetsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPut("engageregion/bulk")]
    public async Task<IActionResult> BulkUpdate(EngageRegionBudgetBulkUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBudgetCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTable(UpdateBudgetsTableCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("CopyTo")]
    public async Task<IActionResult> CopyTo(CopyBudgetCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
