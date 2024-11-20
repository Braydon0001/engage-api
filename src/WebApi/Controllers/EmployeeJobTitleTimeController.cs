using Engage.Application.Services.EmployeeJobTitleTimes.Commands;
using Engage.Application.Services.EmployeeJobTitleTimes.Queries;

namespace Engage.WebApi.Controllers;

public partial class EmployeeJobTitleTimeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<EmployeeJobTitleTimeDto>>> GetProcessList([FromQuery] EmployeeJobTitleTimesQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<EmployeeJobTitleTimeOption>>> Options([FromQuery] EmployeeJobTitleTimeOptionsQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeJobTitleTimeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new EmployeeJobTitleTimeVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(EmployeeJobTitleTimeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.EmployeeJobTitleTimeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EmployeeJobTitleTimeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.EmployeeJobTitleTimeId));
    }

}
