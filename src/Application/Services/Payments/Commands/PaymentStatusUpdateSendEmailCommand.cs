using Engage.Application.Contracts;
using Engage.Application.Services.CommunicationHistories.Commands;
using Engage.Application.Services.Emails.Commands;
using MassTransit;

namespace Engage.Application.Services.Payments.Commands;

public class PaymentStatusUpdateSendEmailCommand : IRequest<OperationStatus>
{
    public string EmailBody { get; init; }
    public List<int> PaymentIds { get; init; }
}

public record PaymentStatusUpdateSendEmailHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator, IBus Bus) : IRequestHandler<PaymentStatusUpdateSendEmailCommand, OperationStatus>, IConsumer<PaymentEmail>
{
    public async Task<OperationStatus> Handle(PaymentStatusUpdateSendEmailCommand command, CancellationToken cancellationToken)
    {
        var firstPayment = await Context.Payments.Include(e => e.PaymentBatch)
                                                 .ThenInclude(b => b.BatchRegions)
                                                 .FirstOrDefaultAsync(e => e.PaymentId == command.PaymentIds.First(), cancellationToken);

        if (firstPayment == null)
        {
            return new OperationStatus();
        }

        var template = await Context.CommunicationTemplates.Where(c => c.CommunicationTemplateTypeId == (int)CommunicationTemplateTypeId.PaymentStatusUpdated &&
                                                                       c.CommunicationTypeId == (int)CommunicationTypeId.Email)
                                                           .FirstOrDefaultAsync(cancellationToken);

        if (template != null)
        {
            var regionIds = firstPayment.PaymentBatch.BatchRegions.Select(e => e.EngageRegionId)
                                                                  .ToList();
            var notificationUsers = await Context.PaymentNotificationStatusUsers.Where(e => e.PaymentStatusId == firstPayment.PaymentStatusId &&
                                                                                   regionIds.Contains(e.EngageRegionId))
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


                await Bus.Publish(new PaymentEmail
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

    public async Task Consume(ConsumeContext<PaymentEmail> context)
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

public class PaymentStatusUpdateSendEmailValidator : AbstractValidator<PaymentStatusUpdateSendEmailCommand>
{
    public PaymentStatusUpdateSendEmailValidator()
    {
        RuleFor(e => e.EmailBody).NotEmpty().MaximumLength(10000);
    }
}