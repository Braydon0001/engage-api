using Engage.Application.Services.EmployeeEmployeeKpis.Commands;
using Engage.Application.Services.EmployeeEmployeeKpis.Models;
using Engage.Application.Services.EmployeeEmployeeKpis.Queries;

namespace Engage.WebApi.Controllers;

public class EmployeeEmployeeKpiController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<ActionResult<ListResult<EmployeeEmployeeKpiDto>>> GetByBadge([FromRoute] EmployeeEmployeeKpiQuery query)
    {
        return Ok(new ListResult<EmployeeEmployeeKpiDto>(await Mediator.Send(query)));
    }

    [HttpGet]
    public async Task<ActionResult<ListResult<EmployeeEmployeeKpiDto>>> GetAll([FromRoute] EmployeeEmployeeKpiQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeEmployeeKpiCreateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EmployeeEmployeeKpiUpdateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("bulk")]
    public async Task<IActionResult> BulkUpdate([FromBody] List<EmployeeEmployeeKpiUpdateCommand> updates, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new EmployeeEmployeeKpiBulkUpdateCommand(updates), cancellationToken));
    }

    //[HttpDelete("{EmployeeId}/{EmployeeKpiId}")]
    //public async Task<IActionResult> Delete([FromRoute] EmployeeEmployeeKpiRemoveCommand command)
    //{
    //    return Ok(await Mediator.Send(command));
    //}

    [HttpDelete()]
    public async Task<IActionResult> Delete(EmployeeEmployeeKpiRemoveCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
