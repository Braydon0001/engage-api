using Engage.Application.Services.EmployeeJobTitleTypes.Commands;
using Engage.Application.Services.EmployeeJobTitleTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class EmployeeJobTitleTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<EmployeeJobTitleTypeDto>>> GetProcessList([FromQuery] EmployeeJobTitleTypesQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<EmployeeJobTitleTypeOption>>> Options([FromQuery] EmployeeJobTitleTypeOptionsQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeJobTitleTypeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new EmployeeJobTitleTypeVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(EmployeeJobTitleTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.EmployeeJobTitleTypeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EmployeeJobTitleTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.EmployeeJobTitleTypeId));
    }

}
