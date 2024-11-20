using Engage.Application.Services.CommunicationHistoryEmployees.Commands;
using Engage.Application.Services.Emails.Commands;

namespace Engage.Application.Services.EmployeeStoreCalendars.Commands;

public class GenerateEmployeeStoreCalendarPreviousPeriodEmailCommand : IRequest<OperationStatus>
{
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeeEmail { get; set; }
    public string ManagerEmail { get; set; }
    public MemoryStream Attachment { get; set; }
    public DateTime ReportDate { get; set; }

}
public class GenerateEmployeeStoreCalendarEmailHandler : BaseCreateCommandHandler, IRequestHandler<GenerateEmployeeStoreCalendarPreviousPeriodEmailCommand, OperationStatus>
{
    private readonly IEmailService _email;
    public GenerateEmployeeStoreCalendarEmailHandler(IAppDbContext context, IMapper mapper, IMediator mediator, IEmailService email) : base(context, mapper, mediator)
    {
        _email = email;
    }

    public async Task<OperationStatus> Handle(GenerateEmployeeStoreCalendarPreviousPeriodEmailCommand request, CancellationToken cancellationToken)
    {
        //var emailVariables = new EmployeeStoreCalendarPreviousPeriodEmailTemplate
        //{
        //    Name = request.EmployeeName,
        //    EmployeeId = request.EmployeeId,
        //    ReportDate = request.ReportDate
        //};

        //return await _email.SendEmailCalendarPreviousPeriodReportAsync(
        //    new EmailModel<EmployeeStoreCalendarPreviousPeriodEmailTemplate>
        //    {
        //        EmailTypeId = EmailTypeId.EmployeeStoreCalendarPreviousPeriodReport,
        //        ToEmail = request.EmployeeEmail,
        //        Subject = "Last Week's store visit report",
        //        //CCEmails = new List<string>() { request.ManagerEmail },
        //        TemplateVariables = emailVariables,
        //        IsSmtp = false,
        //        EmailBody = EmailBody.GetEmployeeStoreCalendarPreviousReportBody(request.EmployeeName),
        //    }, request.Attachment
        //    , cancellationToken);
        var template = await _context.CommunicationTemplates.Where(c => c.CommunicationTemplateTypeId == (int)CommunicationTemplateTypeId.EmployeeStoreCalendarPreviousPeriodReport &&
                                                                        c.CommunicationTypeId == (int)CommunicationTypeId.Email)
                                                            .FirstOrDefaultAsync(cancellationToken);

        if (template != null)
        {
            var templateData = new
            {
                Name = request.EmployeeName,
                EmployeeId = request.EmployeeId,
                ReportDate = request.ReportDate
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
                AttachmentName = $"{DateTime.Now.ToShortDateTimeString()} Last Week's Report.xlsx",
            }, cancellationToken);
        }

        return new OperationStatus { Status = true };
    }
}