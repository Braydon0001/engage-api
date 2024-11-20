using Engage.Application.Services.CommunicationTemplates.Commands;
using Engage.Application.Services.CommunicationTemplates.Queries;

namespace Engage.WebApi.Controllers;

public partial class CommunicationTemplateController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<CommunicationTemplateDto>>> List([FromQuery] CommunicationTemplateListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<CommunicationTemplateDto>(entities));
    }
    [HttpGet("communicationtype/{communicationtypeid}")]
    public async Task<ActionResult<ListResult<CommunicationTemplateDto>>> ListByType([FromRoute] int communicationTypeId, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(new CommunicationTemplateListQuery { CommunicationTypeId = communicationTypeId }, cancellationToken);

        return Ok(new ListResult<CommunicationTemplateDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<CommunicationTemplateOption>>> Options([FromQuery] CommunicationTemplateOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CommunicationTemplateVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new CommunicationTemplateVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(CommunicationTemplateInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.CommunicationTemplateId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(CommunicationTemplateUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.CommunicationTemplateId));
    }

}
