// auto-generated
using Engage.Application.Services.ProductSubGroups.Commands;
using Engage.Application.Services.ProductSubGroups.Queries;

namespace Engage.WebApi.Controllers;

public partial class ProductSubGroupController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ProductSubGroupDto>>> DtoList([FromQuery]ProductSubGroupListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<ProductSubGroupDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<ProductSubGroupOption>>> OptionList([FromQuery]ProductSubGroupOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductSubGroupVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new ProductSubGroupVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProductSubGroupInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.ProductSubGroupId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductSubGroupUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.ProductSubGroupId));
    }


}