using Engage.Application.Services.EmployeeStoreCalendars.Commands;
using Engage.Application.Services.EmployeeStoreCalendars.Queries;
using Engage.Application.Services.SurveyInstances.Commands;
using Engage.WebApi.utils;

namespace Engage.WebApi.Controllers;

public partial class EmployeeStoreCalendarController : BaseController
{
    private readonly IHttpClientFactory _httpClientFactory;

    public EmployeeStoreCalendarController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet("employee/{employeeid}")]
    public async Task<ActionResult<ListResult<EmployeeStoreCalendarDto>>> GetEmployeeStoreCalendarEntries([FromRoute] EmployeeStoreCalendarGetByEmployeeQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("completed/employee/{employeeid}")]
    public async Task<ActionResult<ListResult<EmployeeStoreCalendarByEmployeeDto>>> GetCompletedStoreVisits([FromRoute] EmployeeStoreCalendarCompletedListQuery query)
    {
        var entity = await Mediator.Send(query);
        return Ok(entity);
    }

    [HttpGet("manager/{employeeid}")]
    public async Task<ActionResult<ListResult<EmployeeStoreCalendarManagerViewDto>>> GetEmployeeStoreByManager([FromRoute] EmployeeStoreCalendarGetByManagerQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("manager/week/{employeeid}/{calendarDate}")]
    public async Task<ActionResult<ListResult<EmployeeStoreCalendarWeekDto>>> GetManagerWeek([FromRoute] int employeeid, DateTime calendarDate, [FromQuery] EmployeeStoreCalendarGetManagerWeekQuery query)
    {
        query.EmployeeId = employeeid;
        query.CalendarDate = calendarDate;
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("region/{employeeid}")]
    public async Task<ActionResult<ListResult<EmployeeStoreCalendarManagerViewDto>>> GetEmployeeStoreByRegion([FromRoute] EmployeeStoreCalendarGetByRegionQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("store/{id}")]
    public async Task<ActionResult<EmployeeStoreCalendarDto>> Store([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new EmployeeStoreCalendarVisitQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpGet("surveyform/store")]
    public async Task<ActionResult<EmployeeStoreCalendarSurveyFormsDto>> GetSurveyFormsDto([FromQuery] EmployeeStoreCalendarGetSurveyFormWithAnswersQuery query, CancellationToken cancellationToken)
    {
        if (query.Id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(query, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
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

    [HttpGet("surveyform/instance/{id}")]
    public async Task<ActionResult<EmployeeStoreCalendarSurveySubmissionVm>> GetSurveySubmission([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var survey = await Mediator.Send(new EmployeeStoreCalendarSurveyFormQuery { Id = id }, cancellationToken);

        return survey == null ? NotFound() : Ok(survey);
    }

    [HttpGet("survey/{id}")]
    public async Task<ActionResult<EmployeeStoreCalendarSurveyVm>> SurveyInfo([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new EmployeeStoreCalendarSurveyQuery { Id = id }, cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpGet("employee/{employeeId}/calendarDate/{calendarDate}")]
    public async Task<ActionResult<ListResult<EmployeeStoreCalendarMonthDto>>> GetEmployeeStoreCalendarByPeriod([FromRoute] EmployeeStoreCalendarGetByEmployeeDateQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost("report/previous")]
    public async Task<MemoryStream> GeneratePreviousPeriodReport(GenerateEmployeeStoreCalendarPreviousPeriodReportCommand command)
    {
        var recievedResult = await Mediator.Send(command);

        return ExcelFileGeneratorUtil.GenerateExcelFileStream(
            recievedResult.Count,
            recievedResult.ReportName,
            recievedResult.Data,
            recievedResult.ColumnNames,
            "Employee Store Calendar");
    }

    [HttpPost("report/current")]
    public async Task<MemoryStream> GenerateCurrentPeriodReport(GenerateEmployeeStoreCalendatCurrentPeriodReportCommand command)
    {
        var recievedResult = await Mediator.Send(command);

        return ExcelFileGeneratorUtil.GenerateExcelFileStream(
            recievedResult.Count,
            recievedResult.ReportName,
            recievedResult.Data,
            recievedResult.ColumnNames,
            "Current Week Calendar");
    }

    [AllowAnonymous]
    [HttpPost("email/previous")]
    public async Task<OperationStatus> GeneratePreviousPeriodEmail()
    {
        // get list of people to send previous period report to
        var periodDate = DateTime.Now.AddDays(-3);
        var employees = await Mediator.Send(new EmployeeStoreCalendarGetEmployeesByPeriodQuery { Date = periodDate });

        foreach (var employee in employees)
        {
            //Generate excel document as base 64 string
            var report = await GeneratePreviousPeriodReport(
                new GenerateEmployeeStoreCalendarPreviousPeriodReportCommand
                {
                    EmployeeId = employee.Id,
                    Date = periodDate,
                });

            var response = await Mediator.Send(new GenerateEmployeeStoreCalendarPreviousPeriodEmailCommand
            {
                EmployeeId = employee.Id,
                EmployeeName = $"{employee.EmployeeName}",
                EmployeeEmail = employee.EmployeeEmail,
                ManagerEmail = employee.EmployeeManagerEmail,
                Attachment = report,
                ReportDate = periodDate
            });
        }

        return new OperationStatus(true);
    }

    [AllowAnonymous]
    [HttpPost("email/current")]
    public async Task<OperationStatus> GenerateCurrentPeriodEmail()
    {
        //get list of inFresh specialists
        var currentDate = DateTime.Now;
        var employees = await Mediator.Send(new EmployeeStoreCalendarGetEmployeesByPeriodQuery { Date = currentDate });

        foreach (var employee in employees)
        {
            //generate excel document as base 64 string
            var report = await GenerateCurrentPeriodReport
                (new GenerateEmployeeStoreCalendatCurrentPeriodReportCommand
                {
                    EmployeeId = employee.Id,
                    CurrentDate = currentDate,
                });

            var response = await Mediator.Send(new GenerateEmployeeStoreCalendarCurrentPeriodEmailCommand
            {
                EmployeeId = employee.Id,
                EmployeeName = $"{employee.EmployeeName}",
                EmployeeEmail = employee.EmployeeEmail,
                ManagerEmail = employee.EmployeeManagerEmail,
                Attachment = report,
                CurrentDate = currentDate
            });
        }
        return new OperationStatus(true);
    }

    [HttpGet("target")]
    public async Task<ActionResult<EmployeeStoreCalendarAdvancedTargetingVm>> GetEmployeeStoreCalendarEntries([FromQuery] EmployeeStoreCalendarEmployeeGetTargetingQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("target/{employeeStoreCalendarId}")]
    public async Task<ActionResult<EmployeeStoreCalendarAdvancedTargetingVm>> GetEmployeeStoreCalendarEntriesByStoreVisit([FromRoute] EmployeeStoreCalendarGetTargetingByStoreVisitQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Insert(EmployeeStoreCalendarInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.EmployeeStoreCalendarId));
    } //EmployeeStoreCalendarUpdateSurveysCommand

    [HttpPost("next")]
    public async Task<IActionResult> InsertNext(EmployeeStoreCalendarInsertNextCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.EmployeeStoreCalendarId));
    }

    [HttpPost("surveyform/report/complete")]
    public async Task<IActionResult> SurveyFormStoreVisitReport(EmployeeStoreCalendarGenerateSurveyFormPdfReportQuery command, CancellationToken cancellationToken)
    {
        var httpClient = _httpClientFactory.CreateClient();
        command.HttpClient = httpClient;
        var fileStream = await Mediator.Send(command, cancellationToken);

        var employeeStore = await Mediator.Send(new EmployeeStoreCalendarVmQuery { Id = command.Id }, cancellationToken);

        var contentType = "application/pdf";

        var fileName = $"Contact Report for {employeeStore.StoreId.Name} - {employeeStore.SurveyInstanceCompletionDate.Value.ToShortDateString().Replace('/', '-')}.pdf";
        return File(fileStream, contentType, fileName);
    }

    [HttpPost("report/complete")]
    public async Task<IActionResult> StoreVisitReport(EmployeeStoreCalendarBySurveyVmQuery command, CancellationToken cancellationToken)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var employeeStore = await Mediator.Send(command, cancellationToken);

        var fileStream = await Mediator.Send(new EmployeeStoreCalendarGeneratePdfReportQuery { Id = employeeStore.Id, HttpClient = httpClient }, cancellationToken);

        var contentType = "application/pdf";
        var fileName = $"Contact Report for {employeeStore.StoreId.Name} on {employeeStore.SurveyInstanceCompletionDate.Value.ToShortDateString().Replace('/', '-')}.pdf";
        return File(fileStream, contentType, fileName);
    }

    [HttpPost("email/report")]
    public async Task<IActionResult> EmailStoreReports(EmployeeStoreCalendarEmailReportCommand command, CancellationToken cancellationToken)
    {
        HttpClient httpClient = _httpClientFactory.CreateClient();
        if (command.EmailAddress == "")
        {
            throw new Exception("Please enter an email");
        }
        foreach (int employeeStoreCalendarId in command.Ids)
        {
            var employeeStoreCalendar = await Mediator.Send(new EmployeeStoreCalendarVmQuery { Id = employeeStoreCalendarId }, cancellationToken);


            if (employeeStoreCalendar.SurveyInstanceId != null)
            {
                var pdfStream = await Mediator.Send(new EmployeeStoreCalendarGeneratePdfReportQuery { Id = employeeStoreCalendar.Id, HttpClient = httpClient }, cancellationToken);

                await Mediator.Send(new SurveyInstanceCompleteEmailCommand
                {
                    SurveyInstanceId = employeeStoreCalendar.SurveyInstanceId.Value,
                    EmployeeStoreCalendarId = employeeStoreCalendarId,
                    EmployeeEmail = command.EmailAddress,
                    EmployeeName = employeeStoreCalendar.EmployeeId.Name,
                    StoreName = employeeStoreCalendar.StoreId.Name,
                    SurveyDate = employeeStoreCalendar.CalendarDate.ToShortDateTimeString(),
                    CompletionDate = employeeStoreCalendar.SurveyInstanceCompletionDate ?? DateTime.Now,
                    CcEmails = new List<string>(),
                    Attachment = pdfStream,
                    SaveEmailAddresses = false
                }, cancellationToken);
            }
            else
            {
                //new survey module
                var pdfStream = await Mediator.Send(new EmployeeStoreCalendarGenerateSurveyFormPdfReportQuery { Id = employeeStoreCalendarId, HttpClient = httpClient }, cancellationToken);

                await Mediator.Send(new SurveyInstanceCompleteEmailCommand
                {
                    SurveyInstanceId = null,
                    EmployeeStoreCalendarId = employeeStoreCalendarId,
                    EmployeeEmail = command.EmailAddress,
                    EmployeeName = employeeStoreCalendar.EmployeeId.Name,
                    StoreName = employeeStoreCalendar.StoreId.Name,
                    SurveyDate = employeeStoreCalendar.CalendarDate.ToShortDateTimeString(),
                    CompletionDate = employeeStoreCalendar.SurveyInstanceCompletionDate ?? DateTime.Now,
                    CcEmails = new List<string>(),
                    Attachment = pdfStream,
                    SaveEmailAddresses = false
                }, cancellationToken);
            }
        }
        return Ok(true);
    }

    [HttpPost("employee/month")]
    public async Task<IActionResult> EmployeeReport(EmployeeStoreCalendarEmployeeReportQuery query, CancellationToken cancellationToken)
    {
        var storeVisits = await Mediator.Send(query, cancellationToken);

        var excelStream = ExcelFileGeneratorUtil.GenerateMutliPageExcelStream(
            storeVisits.Data,
            storeVisits.Headings
            );

        var contentType = "application/octet-stream";
        var fileName = $"{storeVisits.FileName}.xlsx";
        return File(excelStream, contentType, fileName);
    }

    [HttpPut]
    public async Task<IActionResult> Update(EmployeeStoreCalendarUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.EmployeeStoreCalendarId));
    }

    [HttpPut("surveyform/update")]
    public async Task<IActionResult> UpdateSurveyForms(EmployeeStoreCalendarUpdateSurveysCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.EmployeeStoreCalendarId));
    }

    [HttpPut("complete")]
    public async Task<IActionResult> CompleteSurvey(EmployeeStoreCalendarCompleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);

        try
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            var employeeStoreCalendar = await Mediator.Send(new EmployeeStoreCalendarVmQuery { Id = command.EmployeeStoreCalendarId }, cancellationToken);
            var file = await Mediator.Send(new EmployeeStoreCalendarGenerateSurveyFormPdfReportQuery
            { Id = command.EmployeeStoreCalendarId, HttpClient = httpClient, SubmissionIds = [command.Id] }, cancellationToken);

            List<string> emails = [];
            if (command.EmailAddresses != null && command.EmailAddresses.Count > 0)
            {
                emails.AddRange(command.EmailAddresses.Select(e => e.Value).ToList());
            }

            await Mediator.Send(new SurveyInstanceCompleteEmailCommand
            {
                SurveyInstanceId = null,
                EmployeeStoreCalendarId = command.EmployeeStoreCalendarId,
                EmployeeEmail = employeeStoreCalendar.EmployeeEmail,
                EmployeeName = employeeStoreCalendar.EmployeeId.Name,
                StoreName = employeeStoreCalendar.StoreId.Name,
                SurveyDate = employeeStoreCalendar.CalendarDate.ToShortDateTimeString(),
                CompletionDate = DateTime.Now,
                CcEmails = emails,
                Attachment = file
            }, cancellationToken);
        }
        catch (Exception)
        {

        }
        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SurveyFormSubmissionId));
    }

    [HttpPut("delete")]
    public async Task<IActionResult> DeleteVisit(EmployeeStoreCalendarDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Mediator.Send(command, cancellationToken);
        return entity == null ? NotFound() : Ok(entity);
    }
}
