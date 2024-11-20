// auto-generated
using Engage.Application.Services.ProductMasterSystemStatuses.Commands;
using Engage.Application.Services.ProductMasterSystemStatuses.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProductMasterSystemStatusController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProductMasterSystemStatusDto>>> DtoList([FromQuery]ProductMasterSystemStatusListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProductMasterSystemStatusDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProductMasterSystemStatusOption>>> OptionList([FromQuery]ProductMasterSystemStatusOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductMasterSystemStatusVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProductMasterSystemStatusVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProductMasterSystemStatusInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProductMasterSystemStatusId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductMasterSystemStatusUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProductMasterSystemStatusId));
    }


}