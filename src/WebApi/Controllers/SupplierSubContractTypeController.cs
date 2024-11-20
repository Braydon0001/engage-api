// auto-generated
using Engage.Application.Services.SupplierSubContractTypes.Commands;
using Engage.Application.Services.SupplierSubContractTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class SupplierSubContractTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SupplierSubContractTypeDto>>> DtoList([FromQuery] SupplierSubContractTypeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SupplierSubContractTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<SupplierSubContractTypeOption>>> OptionList([FromQuery] SupplierSubContractTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierSubContractTypeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SupplierSubContractTypeVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SupplierSubContractTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SupplierSubContractTypeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SupplierSubContractTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SupplierSubContractTypeId));
    }


}