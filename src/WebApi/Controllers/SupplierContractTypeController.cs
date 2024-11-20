// auto-generated
using Engage.Application.Services.SupplierContractTypes.Commands;
using Engage.Application.Services.SupplierContractTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class SupplierContractTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SupplierContractTypeDto>>> DtoList([FromQuery]SupplierContractTypeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SupplierContractTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<SupplierContractTypeOption>>> OptionList([FromQuery]SupplierContractTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierContractTypeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SupplierContractTypeVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SupplierContractTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SupplierContractTypeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SupplierContractTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SupplierContractTypeId));
    }


}