using Engage.Application.Contracts;
using Engage.Application.Services.CommunicationHistories.Commands;
using Engage.Application.Services.Emails.Commands;
using MassTransit;

namespace Engage.Application.Services.Creditors.Commands;

public class CreditorStatusUpdateSendEmailCommand : IRequest<OperationStatus>
{
    public string EmailBody { get; init; }
    public List<int> CreditorIds { get; init; }
}

public record CreditorStatusUpdateSendEmailHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator, IBus Bus) : IRequestHandler<CreditorStatusUpdateSendEmailCommand, OperationStatus>, IConsumer<CreditorEmail>
{
    public async Task<OperationStatus> Handle(CreditorStatusUpdateSendEmailCommand command, CancellationToken cancellationToken)
    {
        var firstCreditor = await Context.Creditors.FirstOrDefaultAsync(e => e.CreditorId == command.CreditorIds.First(), cancellationToken);

        if (firstCreditor == null)
        {
            return new OperationStatus();
        }

        var template = await Context.CommunicationTemplates.Where(c => c.CommunicationTemplateTypeId == (int)CommunicationTemplateTypeId.CreditorStatusUpdated &&
                                                                       c.CommunicationTypeId == (int)CommunicationTypeId.Email)
                                                           .FirstOrDefaultAsync(cancellationToken);

        if (template != null)
        {
            var notificationUsers = await Context.CreditorNotificationStatusUsers.Where(e => e.CreditorStatusId == firstCreditor.CreditorStatusId)
                                                                                 .Select(e => e.User.Email)
                                                                                 .ToListAsync(cancellationToken);

            if (notificationUsers.Count > 0)
            {
                var toEmail = notificationUsers.First();
                notificationUsers.Remove(toEmail);

                await Mediator.Send(new CommunicationHistoryInsertCommand
                {
                    CommunicationTemplateId = template.CommunicationTemplateId,
                    ToEmail = toEmail,
                    FromEmail = template.FromEmailAddress,
                    CcEmails = notificationUsers.Count > 0 ? string.Join(", ", notificationUsers) : null,
                    FromName = template.FromName,
                    Subject = template.Subject,
                    Body = template.Body,
                }, cancellationToken);


                await Bus.Publish(new CreditorEmail
                {
                    ToEmailAddress = toEmail,
                    FromEmailAddress = template.FromEmailAddress,
                    FromEmailName = template.FromName,
                    CcEmailAddresses = notificationUsers,
                    Subject = template.Subject,
                    Body = command.EmailBody,
                }, cancellationToken);
            }
        }

        return new OperationStatus();
    }

    public async Task Consume(ConsumeContext<CreditorEmail> context)
    {
        await Mediator.Send(new SendEmailByBusCommand
        {
            ToEmailAddress = context.Message.ToEmailAddress,
            FromEmailAddress = context.Message.FromEmailAddress,
            FromEmailName = context.Message.FromEmailName,
            CcEmailAddresses = context.Message.CcEmailAddresses,
            Subject = context.Message.Subject,
            Body = context.Message.Body,
        });
    }
}

public class CreditorStatusUpdateSendEmailValidator : AbstractValidator<CreditorStatusUpdateSendEmailCommand>
{
    public CreditorStatusUpdateSendEmailValidator()
    {
        RuleFor(e => e.EmailBody).NotEmpty().MaximumLength(10000);
    }
}