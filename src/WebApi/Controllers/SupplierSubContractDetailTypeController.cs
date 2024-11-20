using Engage.Application.Services.SupplierSubContractDetailTypes.Commands;
using Engage.Application.Services.SupplierSubContractDetailTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class SupplierSubContractDetailTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SupplierSubContractDetailTypeDto>>> DtoList([FromQuery] SupplierSubContractDetailTypeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SupplierSubContractDetailTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<SupplierSubContractDetailTypeOption>>> OptionList([FromQuery] SupplierSubContractDetailTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SupplierSubContractDetailTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SupplierSubContractDetailTypeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SupplierSubContractDetailTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SupplierSubContractDetailTypeId));
    }
}
