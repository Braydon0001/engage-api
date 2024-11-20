// auto-generated
using Engage.Application.Services.ProductMasterStatuses.Commands;
using Engage.Application.Services.ProductMasterStatuses.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProductMasterStatusController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProductMasterStatusDto>>> DtoList([FromQuery]ProductMasterStatusListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProductMasterStatusDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProductMasterStatusOption>>> OptionList([FromQuery]ProductMasterStatusOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductMasterStatusVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProductMasterStatusVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProductMasterStatusInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProductMasterStatusId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductMasterStatusUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProductMasterStatusId));
    }


}