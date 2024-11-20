using Engage.Application.Services.EmployeeStoreCalendars.Queries;
using Engage.Application.Services.SurveyInstances.Commands;
using Engage.Application.Services.SurveyInstances.Models;
using Engage.Application.Services.SurveyInstances.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("survey")]
public class SurveyInstanceController : BaseController
{
    private readonly IOptions<ImageOptions> _imageSettings;
    private readonly IHttpClientFactory _httpClientFactory;

    public SurveyInstanceController(IOptions<ImageOptions> imageSettings, IHttpClientFactory httpClientFactory)
    {
        _imageSettings = imageSettings;
        _httpClientFactory = httpClientFactory;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<ListResult<SurveyInstanceDto>>> DtoList([FromRoute] SurveyInstancesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [AllowAnonymous]
    [HttpGet("complete/{id}")]
    public async Task<ActionResult<ListResult<SurveyInstanceWebAllAnswersDto>>> SurveyInstanceAllAnswersVm([FromRoute] int id)
    {
        if (id == 0)
        {
            return BadRequest(BadIdText);
        }
        var entity = await Mediator.Send(new SurveyInstanceAllAnswersQuery { Id = id });
        return entity == null ? NotFound() : Ok(entity);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<SurveyInstanceVM>> Vm([FromRoute] SurveyInstanceVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [AllowAnonymous]
    [HttpGet("webvm/{id}")]
    public async Task<ActionResult<SurveyInstanceWebVm>> WebVm([FromRoute] int id)
    {
        if (id == 0)
        {
            return BadRequest(BadIdText);
        }
        var entity = await Mediator.Send(new SurveyInstanceWebVmQuery { Id = id });
        return entity == null ? NotFound() : Ok(entity);
    }

    [AllowAnonymous]
    [HttpGet("[action]/{surveyinstanceid}/{questionid}")]
    public async Task<ActionResult<SurveyInstanceWebNextPreviousQuestionDto>> FirstPreviousQuestion([FromRoute] SurveyInstanceNextPreviousQuestionQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Insert(CreateSurveyInstanceCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.SurveyPhotoFolder))
        {
            command.SurveyPhotoFolder = _imageSettings.Value.SurveyPhotoFolder;
        }

        return Ok(await Mediator.Send(command));
    }

    [AllowAnonymous]
    [HttpPut]
    public async Task<IActionResult> Update(UpdateEmployeeStoreSurveyCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [AllowAnonymous]
    [HttpPut("complete")]
    public async Task<IActionResult> UpdateComplete(SurveyInstanceCompleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        try
        {
            var httpClient = _httpClientFactory.CreateClient();
            var employeeStoreCalendar = await Mediator.Send(new EmployeeStoreCalendarBySurveyVmQuery { Id = entity.SurveyInstanceId }, cancellationToken);

            var pdfStream = await Mediator.Send(new EmployeeStoreCalendarGeneratePdfReportQuery { Id = employeeStoreCalendar.Id, HttpClient = httpClient }, cancellationToken);

            if (command.EmailAddresses != null && command.EmailAddresses.Count > 0)
            {
                var emails = command.EmailAddresses.Select(e => e.Value).ToList();
                employeeStoreCalendar.CCEmails.AddRange(emails);
            }

            await Mediator.Send(new SurveyInstanceCompleteEmailCommand
            {
                SurveyInstanceId = entity.SurveyInstanceId,
                EmployeeStoreCalendarId = employeeStoreCalendar.Id,
                EmployeeEmail = employeeStoreCalendar.EmployeeEmail,
                EmployeeName = employeeStoreCalendar.EmployeeId.Name,
                StoreName = employeeStoreCalendar.StoreId.Name,
                SurveyDate = employeeStoreCalendar.CalendarDate,
                CompletionDate = DateTime.Now,
                CcEmails = employeeStoreCalendar.CCEmails,
                Attachment = pdfStream
            }, cancellationToken);
        }
        catch (Exception)
        {
            //do nothing
        }
        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SurveyInstanceId));
    }

}
