using Engage.Application.Services.ClaimEmails.EmailBodies;
using Engage.Application.Services.SurveyInstances.Models;

namespace Engage.Application.Services.ClaimEmailHistories.Commands;

public class ResendSurveyInstanceCompleteEmailManuallyCommand : GetQuery, IRequest<OperationStatus>
{
    public int EmailHistoryId { get; set; }
    public MemoryStream Attachment { get; set; }
}
public class ResendSurveyInstanceCompleteEmailManuallyHandler : BaseQueryHandler, IRequestHandler<ResendSurveyInstanceCompleteEmailManuallyCommand, OperationStatus>
{
    private readonly IEmailService _email;
    public ResendSurveyInstanceCompleteEmailManuallyHandler(IAppDbContext context, IMapper mapper, IEmailService emailService) : base(context, mapper)
    {
        _email = emailService;
    }

    public async Task<OperationStatus> Handle(ResendSurveyInstanceCompleteEmailManuallyCommand request, CancellationToken cancellationToken)
    {
        var emailHistory = await _context.EmailHistories
                                .Include(e => e.EmailHistoryCCEmails)
                                .Include(e => e.EmailHistoryTemplateVariables)
                                .Include(e => e.EmailTemplate)
                                    .ThenInclude(e => e.EmailType)
                                .Where(e => request.EmailHistoryId == e.EmailHistoryId)
                                .SingleOrDefaultAsync(cancellationToken);
        if (emailHistory == null)
        {
            throw new NotFoundException("Email History not found", emailHistory);
        }
        var templateVariable = emailHistory.EmailHistoryTemplateVariables.FirstOrDefault();

        var surveyDate = await _context.SurveyInstances
                    .Where(e => e.SurveyInstanceId == templateVariable.SurveyInstanceId)
                    .Select(e => e.SurveyDate)
                    .FirstOrDefaultAsync(cancellationToken);

        var emailVariables = new SurveyInstanceCompleteEmailTemplate
        {
            Name = templateVariable.Name,
            StoreName = templateVariable.StoreName,
            SurveyInstanceId = templateVariable.SurveyInstanceId.Value
        };
        return await _email.SendEmailContactReportCompleteAsync(
            new EmailModel<SurveyInstanceCompleteEmailTemplate>
            {
                EmailTypeId = EmailTypeId.ContactReportComplete,
                ToEmail = emailHistory.ToEmail,
                CCEmails = emailHistory.EmailHistoryCCEmails.Select(e => e.Email).ToList(),
                Subject = emailHistory.Subject,
                TemplateVariables = emailVariables,
                IsSmtp = true,
                EmailBody = EmailBody.GetContactReportCompleteBody(templateVariable.StoreName, templateVariable.Name)
            },
            request.Attachment,
            $"Contact Report for {templateVariable.StoreName} on {surveyDate.ToShortDateString().Replace('/', '-')}.xlsx",
            cancellationToken
            );
    }
}