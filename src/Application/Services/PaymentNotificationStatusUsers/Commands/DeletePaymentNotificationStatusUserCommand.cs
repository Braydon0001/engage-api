namespace Engage.Application.Services.PaymentNotificationStatusUsers.Commands;

public class DeletePaymentNotificationStatusUserCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class DeletePaymentNotificationStatusUserCommandHandler : IRequestHandler<DeletePaymentNotificationStatusUserCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public DeletePaymentNotificationStatusUserCommandHandler(IAppDbContext context)
    {
        _context = context;
    }
    public async Task<OperationStatus> Handle(DeletePaymentNotificationStatusUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.PaymentNotificationStatusUsers.SingleAsync(s => s.PaymentNotificationStatusUserId == request.Id, cancellationToken);

        _context.PaymentNotificationStatusUsers.Remove(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = request.Id;

        return opStatus;
    }
}
