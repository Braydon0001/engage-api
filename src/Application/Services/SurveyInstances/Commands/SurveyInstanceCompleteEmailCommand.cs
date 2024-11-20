using Engage.Application.Services.CommunicationHistoryEmployeeStoreCalendars.Commands;
using Engage.Application.Services.Emails.Commands;

namespace Engage.Application.Services.SurveyInstances.Commands;

public class SurveyInstanceCompleteEmailCommand : IRequest<OperationStatus>
{
    public int? SurveyInstanceId { get; set; }
    public int EmployeeStoreCalendarId { get; set; }
    public string StoreName { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeeEmail { get; set; }
    public string SurveyDate { get; set; }
    public DateTime CompletionDate { get; set; }
    public List<string> CcEmails { get; set; }
    public MemoryStream Attachment { get; set; }
    public bool SaveEmailAddresses { get; set; } = true;
}
public class SurveyInstanceCompleteEmailHander : BaseUpdateCommandHandler, IRequestHandler<SurveyInstanceCompleteEmailCommand, OperationStatus>
{
    private readonly IEmailService _email;
    public SurveyInstanceCompleteEmailHander(IAppDbContext context, IEmailService email, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
    {
        _email = email;
    }

    public async Task<OperationStatus> Handle(SurveyInstanceCompleteEmailCommand command, CancellationToken cancellationToken)
    {
        //var emailVariables = new SurveyInstanceCompleteEmailTemplate
        //{
        //    Name = command.EmployeeName,
        //    StoreName = command.StoreName,
        //    SurveyInstanceId = command.SurveyInstanceId,
        //    ReportDate = command.CompletionDate.ToShortDateString(),
        //};

        command.CcEmails.RemoveIfContains(command.EmployeeEmail);

        command.CcEmails = command.CcEmails.Distinct().ToList();


        //save email adddresses to employee Store Calendar
        var employeeStoreCalendar = await _context.EmployeeStoreCalendars
                                                  .Where(e => e.EmployeeStoreCalendarId == command.EmployeeStoreCalendarId)
                                                  .FirstOrDefaultAsync(cancellationToken);

        if (command.SaveEmailAddresses)
        {
            var emailAddresses = String.Join(",", command.CcEmails).Truncate(1000 - (command.EmployeeEmail.Length + 1)) + "," + command.EmployeeEmail;

            employeeStoreCalendar.EmailedTo = emailAddresses;
        }

        //return await _email.SendEmailContactReportCompleteAsync(
        //new EmailModel<SurveyInstanceCompleteEmailTemplate>
        //{
        //    EmailTypeId = EmailTypeId.ContactReportComplete,
        //    ToEmail = command.EmployeeEmail,
        //    Subject = $"Contact Report for {command.StoreName}",
        //    TemplateVariables = emailVariables,
        //    CCEmails = command.CcEmails,
        //    IsSmtp = false,
        //    EmailBody = EmailBody.GetContactReportCompleteBody(command.StoreName, command.EmployeeName)
        //},
        //command.Attachment,
        //$"Contact Report for {command.StoreName} on {command.SurveyDate.Replace('/', '-')}.pdf",
        //cancellationToken
        //);
        var template = await _context.CommunicationTemplates.Where(c => c.CommunicationTemplateTypeId == (int)CommunicationTemplateTypeId.ContactReportComplete &&
                                                                        c.CommunicationTypeId == (int)CommunicationTypeId.Email)
                                                            .FirstOrDefaultAsync(cancellationToken);
        if (template != null)
        {
            var templateData = new
            {
                Name = command.EmployeeName,
                StoreName = command.StoreName,
                SurveyInstanceId = command.SurveyInstanceId,
                ReportDate = command.CompletionDate.ToShortDateString(),
            };
            //Save History
            await _mediator.Send(new CommunicationHistoryEmployeeStoreCalendarInsertCommand
            {
                EmployeeStoreCalendarId = command.EmployeeStoreCalendarId,
                CommunicationTemplateId = template.CommunicationTemplateId,
                ToEmail = command.EmployeeEmail,
                FromEmail = template.FromEmailAddress,
                FromName = template.FromName,
                Subject = template.Subject,
                Body = template.Body,
                CcEmails = command.CcEmails.Count > 0 ? string.Join(", ", command.CcEmails) : null,
                TemplateData = templateData,
                HasMemoryStreamAttachment = true,
            }, cancellationToken);

            //Send Email
            await _mediator.Send(new SendEmailCommand
            {
                ToEmailAddress = command.EmployeeEmail,
                FromEmailAddress = template.FromEmailAddress,
                FromEmailName = template.FromName,
                CcEmailAddresses = command.CcEmails,
                Subject = template.Subject,
                Body = template.Body,
                TemplateData = templateData,
                AttachmentStream = command.Attachment,
                AttachmentContentType = "application/pdf",
                AttachmentName = $"Contact Report for {command.StoreName} on {command.SurveyDate.Replace('/', '-')}.pdf",
            }, cancellationToken);
        }

        return new OperationStatus { Status = true };
    }
}
public class SurveyInstanceCompleteEmailValidator : AbstractValidator<SurveyInstanceCompleteEmailCommand>
{
    public SurveyInstanceCompleteEmailValidator()
    {
        RuleFor(e => e.SurveyInstanceId);
        RuleFor(e => e.StoreName).NotNull();
        RuleFor(e => e.EmployeeName);
        RuleFor(e => e.EmployeeEmail);
        RuleFor(e => e.Attachment);
    }
}