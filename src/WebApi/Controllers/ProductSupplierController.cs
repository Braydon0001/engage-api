// auto-generated
using Engage.Application.Services.ProductSuppliers.Commands;
using Engage.Application.Services.ProductSuppliers.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProductSupplierController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProductSupplierDto>>> DtoList([FromQuery]ProductSupplierListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProductSupplierDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProductSupplierOption>>> OptionList([FromQuery]ProductSupplierOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductSupplierVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProductSupplierVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProductSupplierInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProductSupplierId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductSupplierUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProductSupplierId));
    }


}