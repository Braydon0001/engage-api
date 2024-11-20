// auto-generated
using Engage.Application.Services.ProductPeriods.Commands;
using Engage.Application.Services.ProductPeriods.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProductPeriodController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProductPeriodDto>>> DtoList([FromQuery]ProductPeriodListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProductPeriodDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProductPeriodOption>>> OptionList([FromQuery]ProductPeriodOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductPeriodVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProductPeriodVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProductPeriodInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProductPeriodId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductPeriodUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProductPeriodId));
    }


}