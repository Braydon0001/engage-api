// auto-generated
using Engage.Application.Services.EmployeeStoreCalendars.Queries;

namespace Engage.WebApi.Controllers;

public partial class EmployeeStoreCalendarController : BaseController
{
    [HttpPost("page")]
    public async Task<ActionResult<PaginatedListResult<EmployeeStoreCalendarDto>>> PaginatedQuery(EmployeeStoreCalendarPaginatedQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeStoreCalendarVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new EmployeeStoreCalendarVmQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }


}