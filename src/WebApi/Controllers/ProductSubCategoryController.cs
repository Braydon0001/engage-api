// auto-generated
using Engage.Application.Services.ProductSubCategories.Commands;
using Engage.Application.Services.ProductSubCategories.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProductSubCategoryController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProductSubCategoryDto>>> DtoList([FromQuery] ProductSubCategoryListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProductSubCategoryDto>(entities));
    }

    [HttpGet("hierarchy")]
    public async Task<ActionResult<ListResult<ProductSubCategoryHierarchyDto>>> GetHierarchy([FromQuery] ProductSubCategoryHierarchyQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProductSubCategoryOption>>> OptionList([FromQuery] ProductSubCategoryOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductSubCategoryVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProductSubCategoryVmQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProductSubCategoryInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProductSubCategoryId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductSubCategoryUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProductSubCategoryId));
    }


}