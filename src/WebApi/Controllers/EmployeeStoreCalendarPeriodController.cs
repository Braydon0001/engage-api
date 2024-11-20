// auto-generated
using Engage.Application.Services.EmployeeStoreCalendarPeriods.Commands;
using Engage.Application.Services.EmployeeStoreCalendarPeriods.Queries;

namespace Engage.WebApi.Controllers;

public partial class EmployeeStoreCalendarPeriodController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<EmployeeStoreCalendarPeriodDto>>> DtoList([FromQuery]EmployeeStoreCalendarPeriodListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<EmployeeStoreCalendarPeriodDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<EmployeeStoreCalendarPeriodOption>>> OptionList([FromQuery]EmployeeStoreCalendarPeriodOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeStoreCalendarPeriodVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new EmployeeStoreCalendarPeriodVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(EmployeeStoreCalendarPeriodInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.EmployeeStoreCalendarPeriodId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EmployeeStoreCalendarPeriodUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.EmployeeStoreCalendarPeriodId));
    }


}