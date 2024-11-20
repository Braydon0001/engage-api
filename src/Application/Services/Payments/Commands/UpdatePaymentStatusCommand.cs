using Engage.Application.Services.PaymentStatusHistories.Commands;

namespace Engage.Application.Services.Payments.Commands;

public class UpdatePaymentStatusCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public int PaymentStatusId { get; set; }
    public string Reason { get; set; }
    public bool SaveChanges { get; set; } = true;
}

public class UpdatePaymentStatusCommandHandler : IRequestHandler<UpdatePaymentStatusCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;

    public UpdatePaymentStatusCommandHandler(IAppDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }
    public async Task<OperationStatus> Handle(UpdatePaymentStatusCommand command, CancellationToken cancellationToken)
    {
        var payment = await _context.Payments.SingleAsync(x => x.PaymentId == command.Id, cancellationToken);
        payment.PaymentStatusId = command.PaymentStatusId;

        var updateStatusCommand = new PaymentStatusHistoryInsertCommand
        {
            PaymentId = command.Id,
            PaymentStatusId = command.PaymentStatusId,
            Reason = command.Reason,
            SaveChanges = false,
        };

        await _mediator.Send(updateStatusCommand, cancellationToken);

        if (command.SaveChanges)
        {
            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = command.Id;
            return opStatus;
        }

        return new OperationStatus(status: true);
    }
}
