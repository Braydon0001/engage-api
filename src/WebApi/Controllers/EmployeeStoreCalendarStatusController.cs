// auto-generated
using Engage.Application.Services.EmployeeStoreCalendarStatuses.Commands;
using Engage.Application.Services.EmployeeStoreCalendarStatuses.Queries;

namespace Engage.WebApi.Controllers;

public partial class EmployeeStoreCalendarStatusController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<EmployeeStoreCalendarStatusDto>>> DtoList([FromQuery]EmployeeStoreCalendarStatusListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<EmployeeStoreCalendarStatusDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<EmployeeStoreCalendarStatusOption>>> OptionList([FromQuery]EmployeeStoreCalendarStatusOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeStoreCalendarStatusVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new EmployeeStoreCalendarStatusVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(EmployeeStoreCalendarStatusInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.EmployeeStoreCalendarStatusId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EmployeeStoreCalendarStatusUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.EmployeeStoreCalendarStatusId));
    }


}