// auto-generated
using Engage.Application.Services.SupplierSubContractDetails.Commands;
using Engage.Application.Services.SupplierSubContractDetails.Queries;

namespace Engage.WebApi.Controllers;

public partial class SupplierSubContractDetailController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SupplierSubContractDetailDto>>> DtoList([FromQuery] SupplierSubContractDetailListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SupplierSubContractDetailDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<SupplierSubContractDetailOption>>> OptionList([FromQuery] SupplierSubContractDetailOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierSubContractDetailVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SupplierSubContractDetailVmQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpGet("SupplierSubContractTypeId/{supplierSubContractTypeId}")]
    public async Task<ActionResult<SupplierSubContractDetailBySubContractTypeQuery>> ContractDetailsByContractType([FromRoute] SupplierSubContractDetailBySubContractTypeQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SupplierSubContractDetailDto>(entities));
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SupplierSubContractDetailInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SupplierSubContractDetailId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SupplierSubContractDetailUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SupplierSubContractDetailId));
    }

    [HttpPut("disable/{id}")]
    public async Task<IActionResult> Disable([FromRoute] SupplierSubContractDetailDisabledCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);
        return Ok(new OperationStatus(entity.SupplierSubContractDetailId));
    }
}