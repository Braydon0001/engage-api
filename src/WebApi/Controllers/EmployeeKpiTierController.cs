using Engage.Application.Services.EmployeeKpiTiers.Commands;
using Engage.Application.Services.EmployeeKpiTiers.Models;
using Engage.Application.Services.EmployeeKpiTiers.Queries;

namespace Engage.WebApi.Controllers;

public class EmployeeKpiTierController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<ActionResult<EmployeeKpiTierDto>> GetByKpiTier([FromRoute] EmployeeKpiTierVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    public async Task<ActionResult<ListResult<EmployeeKpiTierDto>>> GetAll([FromRoute] EmployeeKpiTierQuery query)
    {
        return Ok(new ListResult<EmployeeKpiTierDto>(await Mediator.Send(query)));
    }

    [HttpGet("options")]
    public async Task<ActionResult<List<OptionDto>>> KpiTierOptionsQuery([FromQuery] EmployeeKpiTierOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option/{EmployeeKpiId}")]
    public async Task<ActionResult<List<OptionDto>>> EmployeeKpiTierOptionsQuery([FromRoute] EmployeeKpiTierOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeKpiTierCreateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EmployeeKpiTierUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
