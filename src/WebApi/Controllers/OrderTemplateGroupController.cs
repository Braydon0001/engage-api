using Engage.Application.Services.OrderTemplateGroups.Commands;
using Engage.Application.Services.OrderTemplateGroups.Queries;

namespace Engage.WebApi.Controllers;

public partial class OrderTemplateGroupController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<OrderTemplateGroupDto>>> DtoList([FromQuery] OrderTemplateGroupListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<OrderTemplateGroupDto>(entities));
    }

    [HttpGet("ordertemplateid/{orderTemplateId}")]
    public async Task<ActionResult<ListResult<OrderTemplateGroupVm>>> VmList([FromRoute] OrderTemplateGroupVmListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<OrderTemplateGroupVm>(entities));
    }

    [HttpGet("orderid/{orderId}")]
    public async Task<ActionResult<ListResult<OrderTemplateGroupOrderSkuVm>>> OrderSkuVmList([FromRoute] OrderTemplateGroupOrderSkuVmListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<OrderTemplateGroupOrderSkuVm>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<OrderTemplateGroupOption>>> OptionList([FromQuery] OrderTemplateGroupOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderTemplateGroupVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new OrderTemplateGroupVmQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(OrderTemplateGroupInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.OrderTemplateGroupId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(OrderTemplateGroupUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.OrderTemplateGroupId));
    }

    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete([FromRoute] int id)
    {
        var entity = await Mediator.Send(new OrderTemplateGroupDeleteCommand { Id = id });

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.OrderTemplateGroupId));
    }

}