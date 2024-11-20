// auto-generated
using Engage.Application.Services.EmployeeStoreCalendarTypes.Commands;
using Engage.Application.Services.EmployeeStoreCalendarTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class EmployeeStoreCalendarTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<EmployeeStoreCalendarTypeDto>>> DtoList([FromQuery]EmployeeStoreCalendarTypeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<EmployeeStoreCalendarTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<EmployeeStoreCalendarTypeOption>>> OptionList([FromQuery]EmployeeStoreCalendarTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeStoreCalendarTypeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new EmployeeStoreCalendarTypeVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(EmployeeStoreCalendarTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.EmployeeStoreCalendarTypeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EmployeeStoreCalendarTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.EmployeeStoreCalendarTypeId));
    }


}