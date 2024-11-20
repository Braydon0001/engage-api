// auto-generated
using Engage.Application.Services.ProductWarehouseSummaries.Commands;
using Engage.Application.Services.ProductWarehouseSummaries.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProductWarehouseSummaryController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProductWarehouseSummaryDto>>> DtoList([FromQuery] ProductWarehouseSummaryListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProductWarehouseSummaryDto>(entities));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductWarehouseSummaryVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProductWarehouseSummaryVmQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProductWarehouseSummaryInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProductWarehouseSummaryId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductWarehouseSummaryUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProductWarehouseSummaryId));
    }

}