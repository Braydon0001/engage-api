// auto-generated
using Engage.Application.Services.SupplierContractDetailTypes.Commands;
using Engage.Application.Services.SupplierContractDetailTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class SupplierContractDetailTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SupplierContractDetailTypeDto>>> DtoList([FromQuery]SupplierContractDetailTypeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SupplierContractDetailTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<SupplierContractDetailTypeOption>>> OptionList([FromQuery]SupplierContractDetailTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierContractDetailTypeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SupplierContractDetailTypeVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SupplierContractDetailTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SupplierContractDetailTypeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SupplierContractDetailTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SupplierContractDetailTypeId));
    }


}