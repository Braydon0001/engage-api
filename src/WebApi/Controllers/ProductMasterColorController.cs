// auto-generated
using Engage.Application.Services.ProductMasterColors.Commands;
using Engage.Application.Services.ProductMasterColors.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProductMasterColorController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProductMasterColorDto>>> DtoList([FromQuery]ProductMasterColorListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProductMasterColorDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProductMasterColorOption>>> OptionList([FromQuery]ProductMasterColorOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductMasterColorVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProductMasterColorVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProductMasterColorInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProductMasterColorId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductMasterColorUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProductMasterColorId));
    }


}