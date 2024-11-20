// auto-generated
using Engage.Application.Services.SupplierSalesLeads.Commands;
using Engage.Application.Services.SupplierSalesLeads.Queries;

namespace Engage.WebApi.Controllers;

public partial class SupplierSalesLeadController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SupplierSalesLeadDto>>> DtoList([FromQuery]SupplierSalesLeadListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SupplierSalesLeadDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<SupplierSalesLeadOption>>> OptionList([FromQuery]SupplierSalesLeadOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierSalesLeadVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SupplierSalesLeadVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SupplierSalesLeadInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SupplierSalesLeadId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SupplierSalesLeadUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SupplierSalesLeadId));
    }


}