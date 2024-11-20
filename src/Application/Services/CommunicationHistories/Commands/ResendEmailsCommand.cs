using Engage.Application.Contracts;
using Engage.Application.Services.Emails.Commands;
using Finbuckle.MultiTenant.Abstractions;
using MassTransit;

namespace Engage.Application.Services.CommunicationHistories.Commands;

public class ResendEmailsCommand : GetQuery, IRequest<OperationStatus>
{
    public int[] CommunicationHistoryIds { get; set; }
}

public class ResendEmailsCommandHandler(IAppDbContext Context, IMediator Mediator, IMultiTenantContextAccessor MultiTenantContextAccessor) : IRequestHandler<ResendEmailsCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ResendEmailsCommand command, CancellationToken cancellationToken)
    {
        var ti = MultiTenantContextAccessor.MultiTenantContext.TenantInfo;
        var emails = await Context.CommunicationHistories.Where(e => command.CommunicationHistoryIds.Contains(e.CommunicationHistoryId))
                                                          .ToListAsync(cancellationToken);

        if (emails.Count > 0)
        {
            foreach (var email in emails)
            {
                await Mediator.Send(new SendSmtpEmailCommand
                {
                    ToEmailAddress = email.ToEmail,
                    FromEmailAddress = email.FromEmail,
                    CcEmailAddresses = string.IsNullOrEmpty(email.CcEmails) ? null : [.. email.CcEmails.Split(',')],
                    Subject = email.Subject,
                    Body = email.Body,
                    AttachmentUrls = string.IsNullOrEmpty(email.AttachmentUrls) ? null : [.. email.AttachmentUrls.Split(',')],
                }, cancellationToken);
            }

            return new OperationStatus { Status = true };
        }
        else
        {
            return new OperationStatus
            {
                Status = false,
                Message = "Could not find any emails to send",
            };
        }
    }

}

public class ResendEmailsConsumer(IAppDbContext Context, IMediator Mediator) : IConsumer<ResendEmail>
{
    public async Task Consume(ConsumeContext<ResendEmail> consumeContext)
    {
        var emails = await Context.CommunicationHistories.Where(e => consumeContext.Message.EmailIds.Contains(e.CommunicationHistoryId))
                                                          .ToListAsync(consumeContext.CancellationToken);

        if (emails.Count > 0)
        {
            foreach (var email in emails)
            {
                await Mediator.Send(new SendSmtpEmailCommand
                {
                    ToEmailAddress = email.ToEmail,
                    FromEmailAddress = email.FromEmail,
                    CcEmailAddresses = string.IsNullOrEmpty(email.CcEmails) ? null : [.. email.CcEmails.Split(',')],
                    Subject = email.Subject,
                    Body = email.Body,
                    AttachmentUrls = string.IsNullOrEmpty(email.AttachmentUrls) ? null : [.. email.AttachmentUrls.Split(',')],
                }, consumeContext.CancellationToken);
            }

            return;
        }
        else
        {
            throw new Exception("Could not find any emails to send");
        }
    }

}


