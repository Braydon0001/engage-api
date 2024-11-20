// auto-generated
using Engage.Application.Services.EmployeeStoreCalendarYears.Commands;
using Engage.Application.Services.EmployeeStoreCalendarYears.Queries;

namespace Engage.WebApi.Controllers;

public partial class EmployeeStoreCalendarYearController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<EmployeeStoreCalendarYearDto>>> DtoList([FromQuery]EmployeeStoreCalendarYearListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<EmployeeStoreCalendarYearDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<EmployeeStoreCalendarYearOption>>> OptionList([FromQuery]EmployeeStoreCalendarYearOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeStoreCalendarYearVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new EmployeeStoreCalendarYearVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(EmployeeStoreCalendarYearInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.EmployeeStoreCalendarYearId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EmployeeStoreCalendarYearUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.EmployeeStoreCalendarYearId));
    }


}