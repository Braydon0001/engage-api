using Engage.Application.Services.ClaimEmails.EmailBodies;
using Engage.Application.Services.SurveyInstances.Models;

namespace Engage.Application.Services.ClaimEmailHistories.Commands;

public class ResendOrderSubmitEmailManuallyCommand : GetQuery, IRequest<OperationStatus>
{
    public int EmailHistoryId { get; set; }
    public MemoryStream Attachment { get; set; }
}
public class ResendOrderSubmitEmailManuallyHandler : BaseQueryHandler, IRequestHandler<ResendOrderSubmitEmailManuallyCommand, OperationStatus>
{
    private readonly IEmailService _email;
    public ResendOrderSubmitEmailManuallyHandler(IAppDbContext context, IMapper mapper, IEmailService emailService) : base(context, mapper)
    {
        _email = emailService;
    }

    public async Task<OperationStatus> Handle(ResendOrderSubmitEmailManuallyCommand request, CancellationToken cancellationToken)
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

        var templateVariables = new SurveyInstanceCompleteEmailTemplate
        {
            Name = templateVariable.Name,
            StoreName = templateVariable.StoreName,
            SurveyInstanceId = templateVariable.SurveyInstanceId.Value
        };

        return await _email.SendEmailOrderSubmitAsync(new EmailModel<SurveyInstanceCompleteEmailTemplate>
        {
            EmailTypeId = EmailTypeId.OrderSubmit,
            ToEmail = emailHistory.ToEmail,
            CCEmails = emailHistory.EmailHistoryCCEmails.Select(e => e.Email).ToList(),
            Subject = emailHistory.Subject,
            TemplateVariables = templateVariables,
            IsSmtp = true,
            EmailBody = EmailBody.GetOrderSubmitBody(templateVariable.Name,
            templateVariable.StoreName,
            templateVariable.OrderDate.Value.ToShortDateString().Replace('/', '-')),
        },
        request.Attachment,
        templateVariable.OrderDate.Value.ToShortDateString().Replace('/', '-'),
        cancellationToken
        );
    }
}