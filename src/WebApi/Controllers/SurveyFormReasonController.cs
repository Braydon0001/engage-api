using Engage.Application.Services.SurveyFormReasons.Commands;
using Engage.Application.Services.SurveyFormReasons.Queries;

namespace Engage.WebApi.Controllers;

public partial class SurveyFormReasonController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SurveyFormReasonDto>>> List([FromQuery] SurveyFormReasonListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SurveyFormReasonDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<SurveyFormReasonOption>>> Options([FromQuery] SurveyFormReasonOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SurveyFormReasonVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SurveyFormReasonVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SurveyFormReasonInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SurveyFormReasonId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SurveyFormReasonUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SurveyFormReasonId));
    }

}
