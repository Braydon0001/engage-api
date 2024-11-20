// auto-generated
using Engage.Application.Services.ProductVendors.Commands;
using Engage.Application.Services.ProductVendors.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProductVendorController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProductVendorDto>>> DtoList([FromQuery]ProductVendorListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProductVendorDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProductVendorOption>>> OptionList([FromQuery]ProductVendorOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductVendorVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProductVendorVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProductVendorInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProductVendorId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductVendorUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProductVendorId));
    }


}