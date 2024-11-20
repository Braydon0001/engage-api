// auto-generated
using Engage.Application.Services.SupplierContractAmountTypes.Commands;
using Engage.Application.Services.SupplierContractAmountTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class SupplierContractAmountTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SupplierContractAmountTypeDto>>> DtoList([FromQuery] SupplierContractAmountTypeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SupplierContractAmountTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<SupplierContractAmountTypeOption>>> OptionList([FromQuery] SupplierContractAmountTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierContractAmountTypeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SupplierContractAmountTypeVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SupplierContractAmountTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SupplierContractAmountTypeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SupplierContractAmountTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SupplierContractAmountTypeId));
    }


}