namespace Engage.Application.Services.Payments.Commands;

public class BatchUpdatePaymentStatusCommand : IRequest<OperationStatus>
{
    public List<int> PaymentIds { get; set; }
    public int? PaymentStatusId { get; set; }
    public string Reason { get; set; }
}

public class BatchUpdatePaymentStatusCommandHandler : IRequestHandler<BatchUpdatePaymentStatusCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;

    public BatchUpdatePaymentStatusCommandHandler(IAppDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<OperationStatus> Handle(BatchUpdatePaymentStatusCommand command, CancellationToken cancellationToken)
    {
        foreach (var id in command.PaymentIds)
        {
            var payment = await _context.Payments.SingleAsync(x => x.PaymentId == id, cancellationToken);

            var updateStatusCommand = new UpdatePaymentStatusCommand
            {
                Id = payment.PaymentId,
                Reason = command.Reason,
                SaveChanges = false,
            };

            if (command.PaymentStatusId.HasValue && command.PaymentStatusId == (int)PaymentStatusId.Rejected)
            {
                updateStatusCommand.PaymentStatusId = (int)PaymentStatusId.Rejected;
            }
            else
            {
                if (payment.PaymentStatusId == (int)PaymentStatusId.New)
                {
                    updateStatusCommand.PaymentStatusId = (int)PaymentStatusId.RegionalApproved;
                }

                if (payment.PaymentStatusId == (int)PaymentStatusId.RegionalApproved)
                {
                    updateStatusCommand.PaymentStatusId = (int)PaymentStatusId.Checked;
                }

                if (payment.PaymentStatusId == (int)PaymentStatusId.Checked)
                {
                    updateStatusCommand.PaymentStatusId = (int)PaymentStatusId.Approved;
                }

                if (payment.PaymentStatusId == (int)PaymentStatusId.Approved)
                {
                    updateStatusCommand.PaymentStatusId = (int)PaymentStatusId.Authorised;
                }

                if (payment.PaymentStatusId == (int)PaymentStatusId.Authorised)
                {
                    updateStatusCommand.PaymentStatusId = (int)PaymentStatusId.Archived;
                }
            }

            await _mediator.Send(updateStatusCommand, cancellationToken);
        }

        return await _context.SaveChangesAsync(cancellationToken);
    }
}
