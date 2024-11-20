using Engage.Application.Services.CommunicationHistoryEmployees.Commands;
using Engage.Application.Services.Emails.Commands;

namespace Engage.Application.Services.EmployeeStoreCalendars.Commands;

public class GenerateEmployeeStoreCalendarCurrentPeriodEmailCommand : IRequest<OperationStatus>
{
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeeEmail { get; set; }
    public string ManagerEmail { get; set; }
    public MemoryStream Attachment { get; set; }
    public DateTime CurrentDate { get; set; }
}
public class GenerateEmployeeStoreCalendarCurrentPeriodEmailHandler : BaseUpdateCommandHandler, IRequestHandler<GenerateEmployeeStoreCalendarCurrentPeriodEmailCommand, OperationStatus>
{
    private readonly IEmailService _email;
    public GenerateEmployeeStoreCalendarCurrentPeriodEmailHandler(IAppDbContext context, IMapper mapper, IEmailService email, IMediator mediator) : base(context, mapper, mediator)
    {
        _email = email;
    }

    public async Task<OperationStatus> Handle(GenerateEmployeeStoreCalendarCurrentPeriodEmailCommand request, CancellationToken cancellationToken)
    {
        //var emailVariables = new EmployeeStoreCalendarCurrentPeriodEmailTemplate
        //{
        //    Name = request.EmployeeName,
        //    EmployeeId = request.EmployeeId,
        //    ReportDate = request.CurrentDate
        //};

        //return await _email.SendEmailCalendarCurrentPeriodReportAsync(
        //    new EmailModel<EmployeeStoreCalendarCurrentPeriodEmailTemplate>
        //    {
        //        EmailTypeId = EmailTypeId.EmployeeStoreCalendarCurrentPeriodReport,
        //        ToEmail = request.EmployeeEmail,
        //        Subject = "Your Store Visits for the week",
        //        //CCEmails = new List<string>() { request.ManagerEmail },
        //        TemplateVariables = emailVariables,
        //        IsSmtp = false,
        //        EmailBody = EmailBody.GetEmployeeStoreCalendarCurrentReportBody(request.EmployeeName),
        //    }, request.Attachment
        //, cancellationToken);
        var template = await _context.CommunicationTemplates.Where(c => c.CommunicationTemplateTypeId == (int)CommunicationTemplateTypeId.EmployeeStoreCalendarCurrentPeriodReport &&
                                                                        c.CommunicationTypeId == (int)CommunicationTypeId.Email)
                                                            .FirstOrDefaultAsync(cancellationToken);

        if (template != null)
        {
            var templateData = new
            {
                Name = request.EmployeeName,
                EmployeeId = request.EmployeeId,
                ReportDate = request.CurrentDate
            };

            //Save History
            await _mediator.Send(new CommunicationHistoryEmployeeInsertCommand
            {
                EmployeeId = request.EmployeeId,
                CommunicationTemplateId = template.CommunicationTemplateId,
                ToEmail = request.EmployeeEmail,
                FromEmail = template.FromEmailAddress,
                FromName = template.FromName,
                Subject = template.Subject,
                Body = template.Body,
                TemplateData = templateData,
                HasMemoryStreamAttachment = true,
            }, cancellationToken);

            //Send Email
            await _mediator.Send(new SendEmailCommand
            {
                ToEmailAddress = request.EmployeeEmail,
                FromEmailAddress = template.FromEmailAddress,
                FromEmailName = template.FromName,
                Subject = template.Subject,
                Body = template.Body,
                TemplateData = templateData,
                AttachmentStream = request.Attachment,
                AttachmentContentType = "application/octet-stream",
                AttachmentName = $"{DateTime.Now.ToShortDateString()} - Report.xlsx",
            }, cancellationToken);
        }

        return new OperationStatus { Status = true };
    }
}

