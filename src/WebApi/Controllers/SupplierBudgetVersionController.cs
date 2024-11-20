// auto-generated
using Engage.Application.Services.SupplierBudgetVersions.Commands;
using Engage.Application.Services.SupplierBudgetVersions.Queries;

namespace Engage.WebApi.Controllers;

public partial class SupplierBudgetVersionController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SupplierBudgetVersionDto>>> DtoList([FromQuery]SupplierBudgetVersionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SupplierBudgetVersionDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<SupplierBudgetVersionOption>>> OptionList([FromQuery]SupplierBudgetVersionOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierBudgetVersionVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SupplierBudgetVersionVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SupplierBudgetVersionInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SupplierBudgetVersionId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SupplierBudgetVersionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SupplierBudgetVersionId));
    }


}