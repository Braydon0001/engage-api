// auto-generated
using Engage.Application.Services.ProductPackSizeTypes.Commands;
using Engage.Application.Services.ProductPackSizeTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProductPackSizeTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProductPackSizeTypeDto>>> DtoList([FromQuery]ProductPackSizeTypeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProductPackSizeTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProductPackSizeTypeOption>>> OptionList([FromQuery]ProductPackSizeTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductPackSizeTypeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProductPackSizeTypeVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProductPackSizeTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProductPackSizeTypeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductPackSizeTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProductPackSizeTypeId));
    }


}