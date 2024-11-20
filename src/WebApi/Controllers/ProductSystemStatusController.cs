// auto-generated
using Engage.Application.Services.ProductSystemStatuses.Commands;
using Engage.Application.Services.ProductSystemStatuses.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProductSystemStatusController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProductSystemStatusDto>>> DtoList([FromQuery]ProductSystemStatusListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProductSystemStatusDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProductSystemStatusOption>>> OptionList([FromQuery]ProductSystemStatusOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductSystemStatusVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProductSystemStatusVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProductSystemStatusInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProductSystemStatusId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductSystemStatusUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProductSystemStatusId));
    }


}