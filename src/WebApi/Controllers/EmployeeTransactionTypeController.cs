// auto-generated
using Engage.Application.Services.EmployeeTransactionTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class EmployeeTransactionTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<EmployeeTransactionTypeDto>>> DtoList([FromQuery]EmployeeTransactionTypeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<EmployeeTransactionTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<EmployeeTransactionTypeOption>>> OptionList([FromQuery]EmployeeTransactionTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeTransactionTypeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new EmployeeTransactionTypeVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }


}