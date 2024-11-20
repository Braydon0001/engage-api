// auto-generated
using Engage.Application.Services.OrderTemplateStatuses.Queries;

namespace Engage.WebApi.Controllers;

public partial class OrderTemplateStatusController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<OrderTemplateStatusDto>>> DtoList([FromQuery]OrderTemplateStatusListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<OrderTemplateStatusDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<OrderTemplateStatusOption>>> OptionList([FromQuery]OrderTemplateStatusOptionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderTemplateStatusVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new OrderTemplateStatusVmQuery { Id = id }, cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }


}