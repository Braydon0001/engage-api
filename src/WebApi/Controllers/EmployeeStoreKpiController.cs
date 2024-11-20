using Engage.Application.Services.EmployeeStoreKpis.Commands;
using Engage.Application.Services.EmployeeStoreKpis.Models;
using Engage.Application.Services.EmployeeStoreKpis.Queries;

namespace Engage.WebApi.Controllers;

public class EmployeeStoreKpiController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<ActionResult<ListResult<EmployeeStoreKpiDto>>> GetByEmployee([FromRoute] EmployeeStoreKpiQuery query)
    {
        return Ok(new ListResult<EmployeeStoreKpiDto>(await Mediator.Send(query)));
    }

    [HttpGet]
    public async Task<ActionResult<ListResult<EmployeeStoreKpiDto>>> GetAll([FromRoute] EmployeeStoreKpiQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeStoreKpiCreateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EmployeeStoreKpiUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{EmployeeId}/{EmployeeKpiId}")]
    public async Task<IActionResult> Delete([FromRoute] EmployeeStoreKpiRemoveCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
