using Engage.Application.Services.SurveyFormSubmissions.Commands;
using Engage.Application.Services.SurveyFormSubmissions.Queries;

namespace Engage.WebApi.Controllers;

public partial class SurveyFormSubmissionController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<SurveyFormSubmissionDto>>> List([FromQuery] SurveyFormSubmissionListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SurveyFormSubmissionDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<SurveyFormSubmissionOption>>> Options([FromQuery] SurveyFormSubmissionOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SurveyFormSubmissionVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SurveyFormSubmissionVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpGet("summary/{id}")]
    public async Task<ActionResult<SurveyFormSubmissionSummaryVm>> GetSurveySummary([FromRoute] SurveyFormSubmissionSummaryQuery query, [FromQuery] bool fullSurvey, CancellationToken cancellationToken)
    {
        query.FullSurvey = fullSurvey;
        var entity = await Mediator.Send(query, cancellationToken);
        return Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SurveyFormSubmissionInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SurveyFormSubmissionId));
    }

    [HttpPut("concept/answers")]
    public async Task<IActionResult> BulkAnswer(SurveyFormSubmissionConceptBulkAnswerCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(command.SurveyFormSubmissionId, entity));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SurveyFormSubmissionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SurveyFormSubmissionId));
    }

}
