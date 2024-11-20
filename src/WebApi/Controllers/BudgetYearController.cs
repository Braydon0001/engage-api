using Engage.Application.Services.BudgetYears.Commands;
using Engage.Application.Services.BudgetYears.Models;
using Engage.Application.Services.BudgetYears.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("budget")]
public class BudgetYearController : BaseController
{

    [HttpGet]
    public async Task<ActionResult<ListResult<BudgetYearVm>>> GetAll([FromRoute] BudgetYearsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BudgetYearVm>> GetVm([FromRoute] BudgetYearVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Insert(CreateBudgetYearCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateBudgetYearCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
