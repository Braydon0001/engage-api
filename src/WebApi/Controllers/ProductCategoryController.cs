// auto-generated
using Engage.Application.Services.ProductCategories.Commands;
using Engage.Application.Services.ProductCategories.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProductCategoryController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProductCategoryDto>>> DtoList([FromQuery]ProductCategoryListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProductCategoryDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProductCategoryOption>>> OptionList([FromQuery]ProductCategoryOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductCategoryVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProductCategoryVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProductCategoryInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProductCategoryId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductCategoryUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProductCategoryId));
    }


}