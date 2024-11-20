using Engage.Application.Services.CommunicationTemplateTypes.Commands;
using Engage.Application.Services.CommunicationTemplateTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class CommunicationTemplateTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<CommunicationTemplateTypeDto>>> List([FromQuery] CommunicationTemplateTypeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<CommunicationTemplateTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<CommunicationTemplateTypeOption>>> Options([FromQuery] CommunicationTemplateTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CommunicationTemplateTypeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new CommunicationTemplateTypeVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(CommunicationTemplateTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.CommunicationTemplateTypeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(CommunicationTemplateTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.CommunicationTemplateTypeId));
    }

}
