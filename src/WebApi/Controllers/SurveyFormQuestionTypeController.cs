using Engage.Application.Services.SurveyFormQuestionTypes.Commands;
using Engage.Application.Services.SurveyFormQuestionTypes.Queries;

namespace Engage.WebApi.Controllers;

public partial class SurveyFormQuestionTypeController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SurveyFormQuestionTypeDto>>> List([FromQuery] SurveyFormQuestionTypeListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SurveyFormQuestionTypeDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<SurveyFormQuestionTypeOption>>> Options([FromQuery] SurveyFormQuestionTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SurveyFormQuestionTypeVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SurveyFormQuestionTypeVmQuery(id), cancellationToken);
        
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SurveyFormQuestionTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SurveyFormQuestionTypeId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SurveyFormQuestionTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SurveyFormQuestionTypeId));
    }

}
