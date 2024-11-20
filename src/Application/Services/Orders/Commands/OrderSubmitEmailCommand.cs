using Engage.Application.Services.CommunicationHistoryOrders.Commands;
using Engage.Application.Services.Emails.Commands;

namespace Engage.Application.Services.Orders.Commands;

public class OrderSubmitEmailCommand : IRequest<OperationStatus>
{
    public string StoreName { get; set; }
    public string OrderDate { get; set; }
    public string EmailAddress { get; set; }
    public int OrderId { get; set; }
    public MemoryStream Attachment { get; set; }
    public List<string> CCEmails { get; set; }
}
public class OrderSubmitEmailHandler : BaseUpdateCommandHandler, IRequestHandler<OrderSubmitEmailCommand, OperationStatus>
{
    private readonly IEmailService _email;
    private readonly IUserService _user;
    public OrderSubmitEmailHandler(IAppDbContext context, IEmailService email, IMapper mapper, IMediator mediator, IUserService user) : base(context, mapper, mediator)
    {
        _email = email;
        _user = user;
    }

    public async Task<OperationStatus> Handle(OrderSubmitEmailCommand command, CancellationToken cancellationToken)
    {
        var user = await _context.Users
                        .Where(e => e.Email == _user.UserName)
                        .FirstOrDefaultAsync(cancellationToken);
        var userName = user.FullName;
        //var emailVariables = new OrderSubmitEmailTemplate
        //{
        //    Name = userName,
        //    StoreName = command.StoreName,
        //    OrderDate = command.OrderDate,
        //    OrderId = command.OrderId,
        //};

        command.CCEmails.RemoveIfContains(command.EmailAddress);

        command.CCEmails = command.CCEmails.Distinct().ToList();

        //save EmailTo field
        var order = await _context.Orders.FirstOrDefaultAsync(e => e.OrderId == command.OrderId, cancellationToken);

        var emailAddresses = String.Join(",", command.CCEmails).Truncate(1000 - (command.EmailAddress.Length + 1)) + "," + command.EmailAddress;

        order.EmailedTo = emailAddresses;

        //return await _email.SendEmailOrderSubmitAsync(
        //new EmailModel<OrderSubmitEmailTemplate>
        //{
        //    EmailTypeId = EmailTypeId.OrderSubmit,
        //    ToEmail = command.EmailAddress,
        //    Subject = $"New Order Submitted {command.OrderDate}",
        //    TemplateVariables = emailVariables,
        //    IsSmtp = false,
        //    CCEmails = command.CCEmails,
        //    EmailBody = EmailBody.GetOrderSubmitBody(userName, command.StoreName, command.OrderDate)
        //},
        //command.Attachment,
        //command.OrderDate,
        //cancellationToken
        //);
        var template = await _context.CommunicationTemplates.Where(c => c.CommunicationTemplateTypeId == (int)CommunicationTemplateTypeId.OrderSubmission &&
                                                                        c.CommunicationTypeId == (int)CommunicationTypeId.Email)
                                                            .FirstOrDefaultAsync(cancellationToken);

        if (template != null)
        {
            var templateData = new
            {
                Name = userName,
                StoreName = command.StoreName,
                OrderDate = command.OrderDate,
                OrderId = command.OrderId,
                OrderCreator = order.CreatedBy,
            };

            //Save History
            await _mediator.Send(new CommunicationHistoryOrderInsertCommand
            {
                OrderId = command.OrderId,
                CommunicationTemplateId = template.CommunicationTemplateId,
                ToEmail = command.EmailAddress,
                FromEmail = template.FromEmailAddress,
                FromName = template.FromName,
                Subject = template.Subject,
                Body = template.Body,
                CcEmails = command.CCEmails.Count > 0 ? string.Join(", ", command.CCEmails) : null,
                TemplateData = templateData,
                HasMemoryStreamAttachment = true,
            }, cancellationToken);

            //Send Email
            await _mediator.Send(new SendEmailCommand
            {
                ToEmailAddress = command.EmailAddress,
                FromEmailAddress = template.FromEmailAddress,
                FromEmailName = template.FromName,
                Subject = template.Subject,
                Body = template.Body,
                CcEmailAddresses = command.CCEmails,
                TemplateData = templateData,
                AttachmentStream = command.Attachment,
                AttachmentContentType = "application/octet-stream",
                AttachmentName = $"Order Summary {command.OrderDate}.pdf",
            }, cancellationToken);
        }

        return new OperationStatus { Status = true };
    }
}
