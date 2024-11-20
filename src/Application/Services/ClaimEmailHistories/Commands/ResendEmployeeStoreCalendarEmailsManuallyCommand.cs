using Engage.Application.Services.ClaimEmails.EmailBodies;
using Engage.Application.Services.EmployeeStoreCalendars.Commands;

namespace Engage.Application.Services.ClaimEmailHistories.Commands;

public class ResendEmployeeStoreCalendarEmailsManuallyCommand : GetQuery, IRequest<OperationStatus>
{
    public int EmailHistoryID { get; set; }
    public MemoryStream Attachment { get; set; }
    public EmailTypeId CurrentEmailTypeId { get; set; }
}
public class ResendEmployeeStoreCalendarEmailsManuallyCommandHandler : BaseQueryHandler, IRequestHandler<ResendEmployeeStoreCalendarEmailsManuallyCommand, OperationStatus>
{
    private readonly IMediator _mediator;
    private readonly IEmailService _email;
    public ResendEmployeeStoreCalendarEmailsManuallyCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator, IEmailService emailService) : base(context, mapper)
    {
        _mediator = mediator;
        _email = emailService;
    }

    public async Task<OperationStatus> Handle(ResendEmployeeStoreCalendarEmailsManuallyCommand request, CancellationToken cancellationToken)
    {
        var emailHistory = await _context.EmailHistories
                                    .Include(e => e.EmailHistoryCCEmails)
                                    .Include(e => e.EmailHistoryTemplateVariables)
                                    .Include(e => e.EmailTemplate)
                                     .ThenInclude(e => e.EmailType)
                                    .Where(e => request.EmailHistoryID == e.EmailHistoryId)
                                    .SingleOrDefaultAsync(cancellationToken);
        if (emailHistory == null)
        {
            throw new NotFoundException("Email History not found", emailHistory);
        }
        var templateVariable = emailHistory.EmailHistoryTemplateVariables.FirstOrDefault();

        switch (request.CurrentEmailTypeId)
        {
            case EmailTypeId.EmployeeStoreCalendarPreviousPeriodReport:
                {
                    var templateVariables = new EmployeeStoreCalendarPreviousPeriodEmailTemplate
                    {
                        Name = templateVariable.EmployeeName,
                        EmployeeId = (int)templateVariable.EmployeeId,
                        ReportDate = (DateTime)templateVariable.ReportDate
                    };
                    return await _email.SendEmailCalendarPreviousPeriodReportAsync(
                        new EmailModel<EmployeeStoreCalendarPreviousPeriodEmailTemplate>
                        {
                            EmailTypeId = EmailTypeId.EmployeeStoreCalendarPreviousPeriodReport,
                            ToEmail = emailHistory.ToEmail,
                            CCEmails = emailHistory.EmailHistoryCCEmails.Select(e => e.Email).ToList(),
                            Subject = emailHistory.Subject,
                            TemplateVariables = templateVariables,
                            IsSmtp = true,
                            EmailBody = EmailBody.GetEmployeeStoreCalendarPreviousReportBody(templateVariable.EmployeeName)
                        }, request.Attachment
                        , cancellationToken);
                }
            case EmailTypeId.EmployeeStoreCalendarCurrentPeriodReport:
                {
                    var templateVariables = new EmployeeStoreCalendarCurrentPeriodEmailTemplate
                    {
                        Name = templateVariable.EmployeeName,
                        EmployeeId = (int)templateVariable.EmployeeId,
                        ReportDate = (DateTime)templateVariable.ReportDate
                    };
                    return await _email.SendEmailCalendarCurrentPeriodReportAsync(new EmailModel<EmployeeStoreCalendarCurrentPeriodEmailTemplate>
                    {
                        EmailTypeId = EmailTypeId.EmployeeStoreCalendarCurrentPeriodReport,
                        ToEmail = emailHistory.ToEmail,
                        CCEmails = emailHistory.EmailHistoryCCEmails.Select(e => e.Email).ToList(),
                        Subject = emailHistory.Subject,
                        TemplateVariables = templateVariables,
                        IsSmtp = true,
                        EmailBody = EmailBody.GetEmployeeStoreCalendarCurrentReportBody(templateVariable.EmployeeName)
                    }, request.Attachment
                    , cancellationToken);
                }
        }

        return new OperationStatus
        {
            Status = true
        };
    }
}