using Engage.Application.Services.StoreConcepts.Commands;
using Engage.Application.Services.StoreConcepts.Models;
using Engage.Application.Services.StoreConcepts.Queries;

namespace Engage.WebApi.Controllers;

public class StoreConceptController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<StoreConceptDto>>> GetAll([FromQuery] StoreConceptsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StoreConceptVm>> GetVm([FromRoute] StoreConceptVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("surveyform/{id}")]
    public async Task<ActionResult<StoreConceptSurveyFormAdvancedTargetingVm>> GetSurveys([FromRoute] SurveyFormConceptGetByTargetQuery query, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(query, cancellationToken));
    }

    [HttpGet("surveyinstance/{storeId}")]
    public async Task<ActionResult<SurveyFormConceptInstanceVm>> GetSurveyInstance([FromRoute] int storeId, [FromQuery] SurveyFormConceptGetConceptInstanceQuery query, CancellationToken cancellationToken)
    {
        query.StoreId = storeId;
        return Ok(await Mediator.Send(query, cancellationToken));
    }

    [HttpGet("surveyform/summary/{id}")]
    public async Task<ActionResult<SurveyFormConcepSummaryVm>> GetSurveySummary([FromRoute] SurveyFormConceptSummaryQuery query, [FromQuery] bool fullSurvey, CancellationToken cancellationToken)
    {
        query.FullSurvey = fullSurvey;
        var entity = await Mediator.Send(query, cancellationToken);
        return Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateStoreConceptCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("surveyform")]
    public async Task<IActionResult> InsertSubmission(SurveyFormInsertSubmissionCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);
        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SurveyFormSubmissionId));
    }

    [HttpPut("surveyform/complete")]
    public async Task<IActionResult> CompleteConcept(SurveyFormConceptCompleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPut("surveyform/answer")]
    public async Task<IActionResult> UpdateAnswer(SurveyFormConceptUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);
        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SurveyFormAnswerId));
    }

    [HttpPut("answer/update")]
    public async Task<IActionResult> UpdateAnswerWithoutRules(SurveyFormUpdateAnswerCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);
        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SurveyFormAnswerHistoryId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateStoreConceptCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("file")]
    public async Task<ActionResult<OperationStatus>> UploadFile([FromForm] StoreConceptUploadFileCommand command)
    {
        var result = await Mediator.Send(command);

        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] int id, [FromQuery] string fileName, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new StoreConceptDeleteFileCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(fileName),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result.Status == false ? NotFound() : Ok(new OperationStatus(id));
    }
}
