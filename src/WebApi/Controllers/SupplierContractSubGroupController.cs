// auto-generated
using Engage.Application.Services.SupplierContractSubGroups.Commands;
using Engage.Application.Services.SupplierContractSubGroups.Queries;

namespace Engage.WebApi.Controllers;

public partial class SupplierContractSubGroupController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SupplierContractSubGroupDto>>> DtoList([FromQuery]SupplierContractSubGroupListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SupplierContractSubGroupDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<SupplierContractSubGroupOption>>> OptionList([FromQuery]SupplierContractSubGroupOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierContractSubGroupVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SupplierContractSubGroupVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SupplierContractSubGroupInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SupplierContractSubGroupId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SupplierContractSubGroupUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SupplierContractSubGroupId));
    }


}