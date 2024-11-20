// auto-generated
using Engage.Application.Services.ProductModuleStatuses.Commands;
using Engage.Application.Services.ProductModuleStatuses.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProductModuleStatusController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProductModuleStatusDto>>> DtoList([FromQuery]ProductModuleStatusListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProductModuleStatusDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProductModuleStatusOption>>> OptionList([FromQuery]ProductModuleStatusOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductModuleStatusVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProductModuleStatusVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProductModuleStatusInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProductModuleStatusId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductModuleStatusUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProductModuleStatusId));
    }


}