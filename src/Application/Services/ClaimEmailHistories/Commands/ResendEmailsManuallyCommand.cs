using Engage.Application.Services.ClaimEmails.EmailBodies;
using Engage.Application.Services.ClaimEmails.Models;
using Engage.Application.Services.Employees.Models;

namespace Engage.Application.Services.ClaimEmailHistories.Commands;

public class ResendEmailsManuallyCommand : GetQuery, IRequest<OperationStatus>
{
    public int[] EmailHistoryIDs { get; set; }
}

public class ResendEmailsManuallyCommandHandler : BaseQueryHandler, IRequestHandler<ResendEmailsManuallyCommand, OperationStatus>
{
    private readonly IMediator _mediator;
    private readonly IEmailService _email;
    public ResendEmailsManuallyCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator, IEmailService emailService) : base(context, mapper)
    {
        _mediator = mediator;
        _email = emailService;
    }

    public async Task<OperationStatus> Handle(ResendEmailsManuallyCommand command, CancellationToken cancellationToken)
    {
        var claimEmailHistories = await _context.EmailHistories
                                                               .Include(e => e.EmailHistoryCCEmails)
                                                               .Include(e => e.EmailHistoryTemplateVariables)
                                                               .Include(e => e.EmailTemplate)
                                                               .ThenInclude(e => e.EmailType)
                                                               .Where(e => command.EmailHistoryIDs.Contains(e.EmailHistoryId))
                                                               .ToListAsync(cancellationToken);

        if (claimEmailHistories.Count > 0)
        {
            foreach (var emailHistory in claimEmailHistories)
            {
                var templateVariable = emailHistory.EmailHistoryTemplateVariables.FirstOrDefault();

                switch (emailHistory.EmailTemplate.EmailTypeId)
                {
                    case (int)EmailTypeId.ClaimRejected:
                        {
                            var templateVariables = new ClaimTemplateVariables
                            {
                                Name = templateVariable.Name,
                                ApproverName = templateVariable.ApproverName,
                                ClaimNumber = templateVariable.ClaimNumber,
                                RejectedReason = templateVariable.RejectedReason,
                            };

                            await _email.SendEmail2Async(new EmailModel<ClaimTemplateVariables>
                            {
                                EmailTypeId = EmailTypeId.ClaimRejected,
                                ToEmail = emailHistory.ToEmail,
                                CCEmails = emailHistory.EmailHistoryCCEmails.Select(e => e.Email).ToList(),
                                Subject = emailHistory.Subject,
                                TemplateVariables = templateVariables,
                                IsSmtp = true,
                                EmailBody = EmailBody.GetRejectedClaimsBody(templateVariable.Name, templateVariable.ApproverName, templateVariable.ClaimNumber, templateVariable.RejectedReason),
                            }, cancellationToken);
                        }
                        break;
                    case (int)EmailTypeId.ClaimApprovalReminder:
                        {
                            var templateVariables = new ClaimTemplateVariables
                            {
                                Name = templateVariable.Name,
                                CutOffDate = templateVariable.CutOffDate,
                            };

                            await _email.SendEmail2Async(new EmailModel<ClaimTemplateVariables>
                            {
                                EmailTypeId = EmailTypeId.ClaimRejected,
                                ToEmail = emailHistory.ToEmail,
                                CCEmails = emailHistory.EmailHistoryCCEmails.Select(e => e.Email).ToList(),
                                Subject = emailHistory.Subject,
                                TemplateVariables = templateVariables,
                                IsSmtp = true,
                                EmailBody = EmailBody.GetClaimApprovalReminderBody(templateVariable.Name, templateVariable.CutOffDate),
                            }, cancellationToken);
                        }
                        break;
                    case (int)EmailTypeId.ClaimPayment:
                        {
                            var claimNos = await _context.EmailTemplateVariableClaimNumbers
                                                               .Where(e => e.EmailHistoryTemplateVariableId == templateVariable.EmailHistoryTemplateVariableId)
                                                               .ToListAsync(cancellationToken);

                            List<ClaimNumber> claimNumbers = new List<ClaimNumber>();
                            if (claimNos != null)
                            {
                                foreach (var claimNumber in claimNos)
                                {
                                    claimNumbers.Add(new ClaimNumber
                                    {
                                        ClaimNo = claimNumber.ClaimNo,
                                        Amount = Math.Round(claimNumber.Amount, 2)
                                    });
                                }
                            }

                            var templateVariables = new ClaimTemplateVariables
                            {
                                Name = templateVariable.Name,
                                TotalAmount = templateVariable.TotalAmount == null ? 0 : Math.Round(Convert.ToDecimal(templateVariable.TotalAmount), 2),
                                ClaimNumbers = claimNumbers,
                            };

                            await _email.SendEmail2Async(new EmailModel<ClaimTemplateVariables>
                            {
                                EmailTypeId = EmailTypeId.ClaimPayment,
                                ToEmail = emailHistory.ToEmail,
                                CCEmails = emailHistory.EmailHistoryCCEmails.Select(e => e.Email).ToList(),
                                Subject = emailHistory.Subject,
                                TemplateVariables = templateVariables,
                                IsSmtp = true,
                                EmailBody = EmailBody.GetClaimPaymentBody(templateVariable.Name, claimNumbers, templateVariable.TotalAmount == null ? 0 : Math.Round(Convert.ToDecimal(templateVariable.TotalAmount), 2)),
                            }, cancellationToken);
                        }
                        break;
                    case (int)EmailTypeId.ClaimDisputed:
                        {
                            var templateVariables = new ClaimTemplateVariables
                            {
                                Name = templateVariable.Name,
                                ApproverName = templateVariable.ApproverName,
                                ClaimNumber = templateVariable.ClaimNumber,
                                DisputedReason = templateVariable.DisputedReason,
                            };

                            await _email.SendEmail2Async(new EmailModel<ClaimTemplateVariables>
                            {
                                EmailTypeId = EmailTypeId.ClaimDisputed,
                                ToEmail = emailHistory.ToEmail,
                                CCEmails = emailHistory.EmailHistoryCCEmails.Select(e => e.Email).ToList(),
                                Subject = emailHistory.Subject,
                                TemplateVariables = templateVariables,
                                IsSmtp = true,
                                EmailBody = EmailBody.GetDisputedClaimsBody(templateVariable.Name, templateVariable.ApproverName, templateVariable.ClaimNumber, templateVariable.DisputedReason),
                            }, cancellationToken);
                        }
                        break;
                    case (int)EmailTypeId.EmployeeTermination:
                        {
                            var templateVariables = new TerminationEmailTemplate
                            {
                                Name = templateVariable.Name,
                                EmployeeName = templateVariable.EmployeeName,
                                TerminationReason = templateVariable.TerminationReason,
                                TerminationDate = templateVariable.TerminationDate,
                                TerminatedBy = templateVariable.TerminatorName,
                            };
                            await _email.SendEmailTerminationAsync(new EmailModel<TerminationEmailTemplate>
                            {
                                EmailTypeId = EmailTypeId.EmployeeTermination,
                                ToEmail = emailHistory.ToEmail,
                                CCEmails = emailHistory.EmailHistoryCCEmails.Select(e => e.Email).ToList(),
                                Subject = emailHistory.Subject,
                                TemplateVariables = templateVariables,
                                IsSmtp = true,
                                EmailBody = EmailBody.GetEmployeeTerminationBody
                                    (templateVariable.Name, templateVariable.EmployeeName, templateVariable.TerminationDate,
                                    templateVariable.TerminationReason, templateVariable.TerminatorName)
                            }, cancellationToken);
                            break;
                        }
                }
            }

            return new OperationStatus
            {
                Status = true,
            };
        }
        else
        {
            return new OperationStatus
            {
                Status = false,
                Message = "Account Managers Not Found",
            };
        }
    }
}
