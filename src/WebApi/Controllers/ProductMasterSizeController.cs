// auto-generated
using Engage.Application.Services.ProductMasterSizes.Commands;
using Engage.Application.Services.ProductMasterSizes.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProductMasterSizeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProductMasterSizeDto>>> DtoList([FromQuery]ProductMasterSizeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProductMasterSizeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProductMasterSizeOption>>> OptionList([FromQuery]ProductMasterSizeOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductMasterSizeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProductMasterSizeVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProductMasterSizeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProductMasterSizeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductMasterSizeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProductMasterSizeId));
    }


}