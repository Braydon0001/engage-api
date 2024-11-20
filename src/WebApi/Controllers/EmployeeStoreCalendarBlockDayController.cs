// auto-generated
using Engage.Application.Services.EmployeeStoreCalendarBlockDays.Queries;

namespace Engage.WebApi.Controllers;

public partial class EmployeeStoreCalendarBlockDayController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<EmployeeStoreCalendarBlockDayDto>>> DtoList([FromQuery] EmployeeStoreCalendarBlockDayListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<EmployeeStoreCalendarBlockDayDto>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeStoreCalendarBlockDayVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new EmployeeStoreCalendarBlockDayVmQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

}