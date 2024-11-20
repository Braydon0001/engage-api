using Engage.Application.Services.EmployeeHealthConditions.Commands;
using Engage.Application.Services.EmployeeHealthConditions.Queries;

namespace Engage.WebApi.Controllers;

public partial class EmployeeHealthConditionController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<EmployeeHealthConditionDto>>> DtoList([FromQuery] EmployeeHealthConditionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<EmployeeHealthConditionDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<EmployeeHealthConditionOption>>> OptionList([FromQuery] EmployeeHealthConditionOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeHealthConditionVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new EmployeeHealthConditionVmQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(EmployeeHealthConditionInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.EmployeeHealthConditionId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(EmployeeHealthConditionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.EmployeeHealthConditionId));
    }


}