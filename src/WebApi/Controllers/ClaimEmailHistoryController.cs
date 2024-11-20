using Engage.Application.Services.ClaimEmailHistories.Commands;
using Engage.Application.Services.ClaimEmailHistories.Models;
using Engage.Application.Services.ClaimEmailHistories.Queries;
using Engage.Application.Services.EmployeeStoreCalendars.Commands;
using Engage.Application.Services.EmployeeStoreCalendars.Queries;
using Engage.Application.Services.Orders.Queries;
using Engage.Application.Services.SurveyInstances.Queries;
using Engage.WebApi.utils;

namespace Engage.WebApi.Controllers;

public class ClaimEmailHistoryController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<PaginatedListResult<ClaimEmailHistoryDto>>> GetPaged([FromRoute] PaginatedClaimEmailHistoryQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("claimperiodid/{claimperiodid}/emailtemplateid/{emailtemplateid}/")]
    public async Task<ActionResult<PaginatedListResult<ClaimEmailHistoryDto>>> GetAllByFilters([FromRoute] int? claimPeriodId, [FromRoute] int? emailTemplateId)
    {
        return Ok(await Mediator.Send(new PaginatedClaimEmailHistoryQuery
        {
            ClaimPeriodId = claimPeriodId,
            EmailTemplateId = emailTemplateId,
        }));
    }

    [HttpPost("ResendEmailsManually")]
    public async Task<IActionResult> ResendEmailsManually([FromBody] ResendEmailsManuallyCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("ResendEmployeeStoreCalendarPeriodEmailsManually")]
    public async Task<IActionResult> ResendEmployeeStoreCalendarCurrentPeriodEmailsManually([FromBody] EmailHistoryGetEmailTemplateQuery command)
    {
        var entities = await Mediator.Send(command);
        foreach (var entity in entities)
        {
            if (entity.EmailTypeId == (int)EmailTypeId.EmployeeStoreCalendarCurrentPeriodReport)
            {
                //Generate current period report
                var report = await GenerateEmployeeStoreCalendarCurrentPeriodReport
                    ((DateTime)entity.EmailHistoryTemplateVariables.ReportDate, (int)entity.EmailHistoryTemplateVariables.EmployeeId);

                await Mediator.Send(new ResendEmployeeStoreCalendarEmailsManuallyCommand
                {
                    EmailHistoryID = entity.Id,
                    Attachment = report,
                    CurrentEmailTypeId = EmailTypeId.EmployeeStoreCalendarCurrentPeriodReport,
                });
            }
            else if (entity.EmailTypeId == (int)EmailTypeId.EmployeeStoreCalendarPreviousPeriodReport)
            {
                //Generate previous period report
                var report = await GenerateEmployeeStoreCalendarPreviousPeriodReport
                    ((DateTime)entity.EmailHistoryTemplateVariables.ReportDate, (int)entity.EmailHistoryTemplateVariables.EmployeeId);
                await Mediator.Send(new ResendEmployeeStoreCalendarEmailsManuallyCommand
                {
                    EmailHistoryID = entity.Id,
                    Attachment = report,
                    CurrentEmailTypeId = EmailTypeId.EmployeeStoreCalendarPreviousPeriodReport,
                });
            }
        }
        return Ok();
    }

    private async Task<MemoryStream> GenerateEmployeeStoreCalendarCurrentPeriodReport(DateTime reportDate, int employeeId)
    {
        var recievedResult = await Mediator.Send(new GenerateEmployeeStoreCalendarPreviousPeriodReportCommand
        {
            Date = reportDate,
            EmployeeId = employeeId,
        });

        return ExcelFileGeneratorUtil.GenerateExcelFileStream(recievedResult.Count
            , recievedResult.ReportName
            , recievedResult.Data
            , recievedResult.ColumnNames
            , "Employee Store Calendar");
    }

    private async Task<MemoryStream> GenerateEmployeeStoreCalendarPreviousPeriodReport(DateTime reportDate, int employeeId)
    {
        var recievedResult = await Mediator.Send(new GenerateEmployeeStoreCalendarPreviousPeriodReportCommand
        {
            Date = reportDate,
            EmployeeId = employeeId,
        });

        return ExcelFileGeneratorUtil.GenerateExcelFileStream(recievedResult.Count
            , recievedResult.ReportName
            , recievedResult.Data
            , recievedResult.ColumnNames
            , "Employee Store Calendar");
    }

    [HttpPost("resend/contactreport")]
    public async Task<IActionResult> ResendContactReportManually([FromBody] EmailHistoryGetEmailTemplateQuery command)
    {
        var entities = await Mediator.Send(command);
        foreach (var entity in entities)
        {
            var surveyAnswers = await Mediator.Send(new SurveyInstanceAllAnswersQuery
            { Id = entity.EmailHistoryTemplateVariables.SurveyInstanceId.Value });

            var employeeStoreCalendar = await Mediator.Send(new EmployeeStoreCalendarBySurveyVmQuery
            { Id = entity.EmailHistoryTemplateVariables.SurveyInstanceId.Value });

            if (surveyAnswers == null)
            {
                throw new Exception("no survey instance found for " + entity.EmailHistoryTemplateVariables.SurveyInstanceId.Value);
            }

            var excelStream = await ExcelFileGeneratorUtil.GenerateCompleteSurveyExcelStream(
                surveyAnswers.Data,
                employeeStoreCalendar,
                entity.EmailHistoryTemplateVariables.ReportDate ?? DateTime.Now);

            await Mediator.Send(new ResendSurveyInstanceCompleteEmailManuallyCommand
            {
                EmailHistoryId = entity.Id,
                Attachment = excelStream,
            });
        }
        return Ok();
    }

    [HttpPost("resend/ordersubmit")]
    public async Task<IActionResult> ResendOrderSubmitManually([FromBody] EmailHistoryGetEmailTemplateQuery command)
    {
        var entities = await Mediator.Send(command);
        foreach (var entity in entities)
        {
            var orderEmailVm = await Mediator.Send(new OrderEmailVmQuery { Id = entity.EmailHistoryTemplateVariables.OrderId.Value });
            var orderVm = await Mediator.Send(new OrderVmQuery { Id = entity.EmailHistoryTemplateVariables.OrderId });

            var attachment = await ExcelFileGeneratorUtil.GenerateOrderSummeryExcelStream(orderVm, orderEmailVm.EngageLogo, orderEmailVm.OrderPlacedBy);

            await Mediator.Send(new ResendOrderSubmitEmailManuallyCommand
            {
                EmailHistoryId = entity.Id,
                Attachment = attachment,
            });
        }
        return Ok();
    }
}
