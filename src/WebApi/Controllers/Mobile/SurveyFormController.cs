using Engage.Application.Services.Mobile.SurveyForms.Queries;
using Engage.Application.Services.SurveyFormAnswers.Commands;
using Engage.Application.Services.SurveyFormSubmissions.Commands;

namespace Engage.WebApi.Controllers.Mobile;

public partial class SurveyFormController : BaseMobileController
{
    [HttpGet("{employeeId}/{storeId}")]
    public async Task<ActionResult<SurveyFormMobileDto>> SupplierSurveys([FromRoute] int employeeId, [FromRoute] int storeId, CancellationToken cancellationToken)
    {
        if (employeeId <= 0 || storeId <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SurveyFormSupplierGroupedQuery() { EmployeeId = employeeId, StoreId = storeId }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpGet("posupdate/{employeeId}/{storeId}")]
    public async Task<ActionResult<SurveyFormMobileHistoryDto>> PosUpdateSurveys([FromRoute] int employeeId, [FromRoute] int storeId, CancellationToken cancellationToken)
    {
        if (employeeId <= 0 || storeId <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SurveyFormPosUpdateGroupedQuery() { EmployeeId = employeeId, StoreId = storeId }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpGet("shelfspacing/{employeeId}/{storeId}")]
    public async Task<ActionResult<SurveyFormMobileDto>> ShelfSpacingSurveys([FromRoute] int employeeId, [FromRoute] int storeId, CancellationToken cancellationToken)
    {
        if (employeeId <= 0 || storeId <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SurveyFormShelfSpacingGroupedQuery() { EmployeeId = employeeId, StoreId = storeId }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpGet("{employeeId}")]
    public async Task<ActionResult<SurveyFormMobileOfflineDto>> RegionSupplierSurveys([FromRoute] int employeeId, [FromQuery] List<string> surveyType, CancellationToken cancellationToken)
    {
        if (employeeId <= 0 || surveyType.Count == 0)
        {
            return BadRequest(surveyType.Count == 0 ? "Survey Types must be provided" : BadIdText);
        }

        var entity = await Mediator.Send(new SurveyFormSupplierGroupedByRegionQuery() { EmployeeId = employeeId, SurveyTypes = surveyType }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpGet("surveyformstorehistory/{employeeId}")]
    public async Task<ActionResult<SurveyFormMobileOfflineDto>> StoreHistorySupplierSurveys([FromRoute] int employeeId, [FromQuery] List<string> surveyType, CancellationToken cancellationToken)
    {
        if (employeeId <= 0)
        {
            return BadRequest(BadIdText);
        }

        if (surveyType.Count == 0)
        {
            surveyType = ["Campaign"];
        }

        var entity = await Mediator.Send(new SurveyFormSupplierStoreHistoryQuery() { EmployeeId = employeeId, SurveyTypes = surveyType }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SurveyFormSubmissionInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SurveyFormSubmissionId));
    }

    [HttpPut("abandon")]
    public async Task<IActionResult> AbandonSurvey(SurveyFormSubmissionAbandonCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SurveyFormSubmissionId));
    }

    [HttpPut("complete")]
    public async Task<IActionResult> CompleteSurvey(SurveyFormSubmissionCompleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);


        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SurveyFormSubmissionId));


    }

    [HttpGet("poshistory/{storeId}")]
    public async Task<ActionResult<List<SurveyFormHistoryDto>>> GetPosHistory([FromRoute] int storeId, CancellationToken cancellationToken)
    {
        if (storeId <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SurveyFormPosHistoryQuery() { StoreId = storeId }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPut]
    public async Task<IActionResult> Update(SurveyFormSubmissionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SurveyFormSubmissionId));
    }

    [HttpPut("answer")]
    public async Task<IActionResult> Insert(SurveyFormSubmissionAnswerCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SurveyFormAnswerId));
    }

    [HttpPut("file")]
    public async Task<IActionResult> FileUpload([FromForm] SurveyFormAnswerFileUploadCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        return result.File == null ? NotFound() : Ok(new OperationStatus(result.Id, result.File));
    }

    [HttpDelete("file")]
    public async Task<ActionResult> FileDelete([FromQuery] string id, [FromQuery] string filename, [FromQuery] string filetype, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new SurveyFormAnswerFileDeleteCommand
        {
            Id = id,
            FileName = HttpUtility.UrlDecode(filename),
            FileType = !string.IsNullOrWhiteSpace(filetype) ? HttpUtility.UrlDecode(filetype) : null
        }, cancellationToken);

        return result.Success == false ? NotFound() : Ok(new OperationStatus(result.Id));
    }

}
