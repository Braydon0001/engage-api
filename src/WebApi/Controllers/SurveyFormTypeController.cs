using Engage.Application.Services.SurveyFormTypes.Commands;
using Engage.Application.Services.SurveyFormTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class SurveyFormTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SurveyFormTypeDto>>> List([FromQuery] SurveyFormTypeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SurveyFormTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<SurveyFormTypeOption>>> Options([FromQuery] SurveyFormTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SurveyFormTypeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SurveyFormTypeVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpGet("survey/{id}")]
    public async Task<ActionResult<SurveyFormTypeVm>> SurveyFormType([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SurveyFormTypeSurveyQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SurveyFormTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SurveyFormTypeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SurveyFormTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SurveyFormTypeId));
    }

}
