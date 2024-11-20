namespace Engage.Application.Services.PaymentLines.Commands;

public class DeletePaymentLineCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class DeletePaymentLineCommandHandler : IRequestHandler<DeletePaymentLineCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public DeletePaymentLineCommandHandler(IAppDbContext context)
    {
        _context = context;
    }
    public async Task<OperationStatus> Handle(DeletePaymentLineCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.PaymentLines.SingleAsync(s => s.PaymentLineId == request.Id, cancellationToken);

        var payment = await _context.Payments.SingleAsync(s => s.PaymentId == entity.PaymentId, cancellationToken);
        if (payment.PaymentStatusId > (int)PaymentStatusId.New)
        {
            throw new Exception("Cannot Delete a Line for payment not in a New Status");
        }

        _context.PaymentLines.Remove(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = request.Id;

        return opStatus;
    }
}
