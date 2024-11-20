namespace Engage.Application.Services.Payments.Commands;

public class DeletePaymentCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class DeletePaymentCommandHandler : IRequestHandler<DeletePaymentCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public DeletePaymentCommandHandler(IAppDbContext context)
    {
        _context = context;
    }
    public async Task<OperationStatus> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Payments.SingleAsync(s => s.PaymentId == request.Id, cancellationToken);

        if (entity.PaymentStatusId > (int)PaymentStatusId.New)
        {
            throw new Exception("Cannot Delete a payment that is not in a New Status");
        }

        _context.Payments.Remove(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = request.Id;

        return opStatus;
    }
}
