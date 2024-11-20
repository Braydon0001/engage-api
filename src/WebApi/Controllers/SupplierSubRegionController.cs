using Engage.Application.Services.SupplierSubRegions.Commands;
using Engage.Application.Services.SupplierSubRegions.Queries;

namespace Engage.WebApi.Controllers;

public partial class SupplierSubRegionController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SupplierSubRegionDto>>> DtoList([FromQuery] SupplierSubRegionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SupplierSubRegionDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<SupplierSubRegionOption>>> OptionList([FromQuery] SupplierSubRegionOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierSubRegionVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SupplierSubRegionVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SupplierSubRegionInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SupplierSubRegionId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SupplierSubRegionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SupplierSubRegionId));
    }


}