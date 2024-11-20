// auto-generated
using Engage.Application.Services.SupplierContractSplits.Commands;
using Engage.Application.Services.SupplierContractSplits.Queries;

namespace Engage.WebApi.Controllers;

public partial class SupplierContractSplitController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SupplierContractSplitDto>>> DtoList([FromQuery] SupplierContractSplitListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SupplierContractSplitDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<SupplierContractSplitOption>>> OptionList([FromQuery] SupplierContractSplitOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierContractSplitVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SupplierContractSplitVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SupplierContractSplitInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SupplierContractSplitId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SupplierContractSplitUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SupplierContractSplitId));
    }


}