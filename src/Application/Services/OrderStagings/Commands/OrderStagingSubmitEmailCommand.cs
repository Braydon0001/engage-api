using Engage.Application.Services.ClaimEmails.EmailBodies;
using Engage.Application.Services.Orders.Models;

namespace Engage.Application.Services.OrderStagings.Commands;

public class OrderStagingSubmitEmailCommand : IRequest<OperationStatus>
{
    public string StoreName { get; set; }
    public string OrderDate { get; set; }
    public string EmailAddress { get; set; }
    public int OrderStagingId { get; set; }
    public MemoryStream Attachment { get; set; }
    public List<string> CCEmails { get; set; }
}
public class OrderStagingSubmitEmailHandler : BaseUpdateCommandHandler, IRequestHandler<OrderStagingSubmitEmailCommand, OperationStatus>
{
    private readonly IEmailService _email;
    private readonly IUserService _user;
    public OrderStagingSubmitEmailHandler(IAppDbContext context, IEmailService email, IMapper mapper, IUserService user) : base(context, mapper)
    {
        _email = email;
        _user = user;
    }

    public async Task<OperationStatus> Handle(OrderStagingSubmitEmailCommand command, CancellationToken cancellationToken)
    {
        var user = await _context.Users
                        .Where(e => e.Email == _user.UserName)
                        .FirstOrDefaultAsync(cancellationToken);
        var userName = user.FullName;
        var emailVariables = new OrderSubmitEmailTemplate
        {
            Name = userName,
            StoreName = command.StoreName,
            OrderDate = command.OrderDate ?? DateTime.Now.Date.ToShortDateString().Replace('/', '-'),
            OrderId = command.OrderStagingId,
        };

        command.CCEmails.RemoveIfContains(command.EmailAddress);

        command.CCEmails = command.CCEmails.Distinct().ToList();

        //save EmailTo field
        var order = await _context.OrderStagings.FirstOrDefaultAsync(e => e.OrderStagingId == command.OrderStagingId, cancellationToken);

        var emailAddresses = String.Join(",", command.CCEmails).Truncate(1000 - (command.EmailAddress.Length + 1)) + "," + command.EmailAddress;

        //order.EmailedTo = emailAddresses;

        return await _email.SendEmailOrderSubmitAsync(
        new EmailModel<OrderSubmitEmailTemplate>
        {
            EmailTypeId = EmailTypeId.OrderSubmit,
            ToEmail = command.EmailAddress,
            Subject = $"Order Submitted {command.OrderDate}",
            TemplateVariables = emailVariables,
            IsSmtp = false,
            CCEmails = command.CCEmails,
            EmailBody = EmailBody.GetOrderSubmitBody(userName, command.StoreName, command.OrderDate)
        },
        command.Attachment,
        command.OrderDate,
        cancellationToken
        );
    }
}
