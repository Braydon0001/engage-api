// auto-generated
using Engage.Application.Services.SupplierSubContracts.Commands;
using Engage.Application.Services.SupplierSubContracts.Queries;

namespace Engage.WebApi.Controllers;

public partial class SupplierSubContractController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SupplierSubContractDto>>> DtoList([FromQuery] SupplierSubContractListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SupplierSubContractDto>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierSubContractVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SupplierSubContractVmQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpGet("suppliercontractId/{supplierContractId}")]
    public async Task<ActionResult<ListResult<SupplierSubContractDto>>> SubContractByContract([FromRoute] SupplierSubContractByContractQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SupplierSubContractDto>(entities));
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SupplierSubContractInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SupplierSubContractId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SupplierSubContractUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SupplierSubContractId));
    }

    [HttpPut("disable/{id}")]
    public async Task<IActionResult> Disabled([FromRoute] SupplierSubContractDisableCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);
        return Ok(new OperationStatus(entity.SupplierSubContractId));
    }
}