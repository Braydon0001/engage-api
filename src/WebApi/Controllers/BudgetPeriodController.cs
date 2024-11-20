using Engage.Application.Services.BudgetPeriods.Commands;
using Engage.Application.Services.BudgetPeriods.Models;
using Engage.Application.Services.BudgetPeriods.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("budget")]
public class BudgetPeriodController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<BudgetPeriodVm>>> DtoList([FromQuery] BudgetPeriodsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("options")]
    public async Task<ActionResult<List<OptionDto>>> OptionList([FromQuery] BudgetPeriodOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BudgetPeriodVm>> GetVm([FromRoute] BudgetPeriodVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBudgetPeriodCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateBudgetPeriodCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
