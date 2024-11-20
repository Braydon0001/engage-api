using Engage.Application.Services.EmployeeStoreCalendarBlockDays.Commands;

namespace Engage.WebApi.Controllers;

public partial class EmployeeStoreCalendarBlockDayController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Insert(EmployeeStoreCalendarBlockDayInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.EmployeeStoreCalendarBlockDayId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EmployeeStoreCalendarBlockDayUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.EmployeeStoreCalendarBlockDayId));
    }

    [HttpPut("delete")]
    public async Task<IActionResult> Delete(EmployeeStoreCalendarBlockDayDeleteCommand command, CancellationToken cancellation)
    {
        var entity = await Mediator.Send(command, cancellation);
        return entity == null ? NotFound() : Ok(entity);
    }
}
