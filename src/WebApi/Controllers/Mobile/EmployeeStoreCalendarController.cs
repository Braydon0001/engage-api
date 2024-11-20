// auto-generated
using Engage.Application.Services.EmployeeStoreCalendarBlockDays.Commands;
using Engage.Application.Services.EmployeeStoreCalendars.Commands;
using Engage.Application.Services.EmployeeStoreCalendars.Queries;

namespace Engage.WebApi.Controllers.Mobile;

public partial class EmployeeStoreCalendarController : BaseMobileController
{
    private readonly IHttpClientFactory _httpClientFactory;
    public EmployeeStoreCalendarController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }


    [HttpGet("surveyform/store/{id}")]
    public async Task<ActionResult<EmployeeStoreCalendarSurveyFormsDto>> GetSurveyForms([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new EmployeeStoreCalendarGetSurveyFormsQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpGet("surveyform/contactreportlist/store/{id}/employee/{employeeId}")]
    public async Task<ActionResult<ListResult<EmployeeStoreCalendarSurveyFormsDto>>> GetContactReportSurveyForms([FromRoute] int id, int employeeId, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(new EmployeeStoreCalendarGetSurveyFormsMobileQuery { Id = id, EmployeeId = employeeId }, cancellationToken);

        return Ok(entity);
    }


    [HttpGet("employee/{employeeId}/calendarDate/{calendarDate}")]
    public async Task<ActionResult<ListResult<EmployeeStoreCalendarMonthDto>>> GetEmployeeStoreCalendarByPeriod([FromRoute] EmployeeStoreCalendarGetByEmployeeDateQuery query)
    {
        return Ok(await Mediator.Send(query));
    }


    [HttpPost]
    public async Task<IActionResult> Insert(EmployeeStoreCalendarInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.EmployeeStoreCalendarId));
    }


    [HttpPut]
    public async Task<IActionResult> Update(EmployeeStoreCalendarUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.EmployeeStoreCalendarId));
    }

    [HttpPost("blockday")]
    public async Task<IActionResult> Insert(EmployeeStoreCalendarBlockDayInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.EmployeeStoreCalendarBlockDayId));
    }

}