using Engage.Application.Services.BudgetYearVersions.Commands;
using Engage.Application.Services.BudgetYearVersions.Models;
using Engage.Application.Services.BudgetYearVersions.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("budget")]
public class BudgetYearVersionController : BaseController
{


    [HttpGet]
    public async Task<ActionResult<ListResult<BudgetYearVersionVm>>> GetAll([FromRoute] BudgetYearsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("budgetYearId/{BudgetYearId}/budgetVersionId/{BudgetVersionId}")]
    public async Task<ActionResult<BudgetYearVersionVm>> GetById([FromRoute] BudgetYearVersionVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBudgetYearVersionCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateBudgetYearVersionCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
