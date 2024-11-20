using Engage.Application.Services.EmployeeKpis.Commands;
using Engage.Application.Services.EmployeeKpis.Models;
using Engage.Application.Services.EmployeeKpis.Queries;

namespace Engage.WebApi.Controllers;

public class EmployeeKpiController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<ActionResult<EmployeeKpiDto>> GetByKpi([FromRoute] EmployeeKpiVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    public async Task<ActionResult<ListResult<EmployeeKpiDto>>> GetAll([FromRoute] EmployeeKpiQuery query)
    {
        return Ok(new ListResult<EmployeeKpiDto>(await Mediator.Send(query)));
    }

    [HttpGet("option")]
    public async Task<ActionResult<List<OptionDto>>> EmployeeKpiOptionsQuery([FromRoute] EmployeeKpiOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("optionnostore")]
    public async Task<ActionResult<List<OptionDto>>> EmployeeKpiOptionsWithoutStoreQuery([FromRoute] EmployeeKpiOptionsNoStoreQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("optionnoemployee")]
    public async Task<ActionResult<List<OptionDto>>> EmployeeKpiOptionsWithoutEmployeeQuery([FromRoute] EmployeeKpiOptionsNoEmployeeQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeKpiCreateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EmployeeKpiUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
