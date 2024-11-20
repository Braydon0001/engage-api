namespace Engage.Application.Services.Payments.Commands;

public class PaymentBatchArchiveCommand : IRequest<OperationStatus>
{
    public List<int> PaymentIds { get; set; }
    public int PaymentStatusId { get; set; }
    public IFormFile[] FilesProofOfPayment { get; set; }
}

public class PaymentBatchArchiveCommandHandler : IRequestHandler<PaymentBatchArchiveCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;
    private readonly IFileService _file;

    public PaymentBatchArchiveCommandHandler(IAppDbContext context, IMediator mediator, IFileService file)
    {
        _context = context;
        _mediator = mediator;
        _file = file;
    }

    public async Task<OperationStatus> Handle(PaymentBatchArchiveCommand command, CancellationToken cancellationToken)
    {
        if (command.PaymentStatusId == (int)PaymentStatusId.Archived)
        {
            var newPaymentArchive = new PaymentArchive { ArchiveDate = DateTime.Now };

            _context.PaymentArchives.Add(newPaymentArchive);

            var opStatus = await _context.SaveChangesAsync(cancellationToken);

            if (opStatus.Status)
            {
                foreach (var id in command.PaymentIds)
                {
                    var payment = await _context.Payments.SingleAsync(x => x.PaymentId == id, cancellationToken);

                    var updateStatusCommand = new UpdatePaymentStatusCommand
                    {
                        Id = payment.PaymentId,
                        PaymentStatusId = command.PaymentStatusId,
                        SaveChanges = false,
                    };

                    payment.PaymentArchiveId = newPaymentArchive.PaymentArchiveId;

                    await _mediator.Send(updateStatusCommand, cancellationToken);
                }

                var proofOfPaymentUpdateCommand = new FileUpdateCommand
                {
                    ContainerName = nameof(PaymentArchive),
                    EntityFiles = newPaymentArchive.Files,
                    MaxFiles = 5,
                    Files = command.FilesProofOfPayment,
                    Id = newPaymentArchive.PaymentArchiveId,
                    FileType = "proofofpayment"
                };

                newPaymentArchive.Files = await _file.UpdateAsync(proofOfPaymentUpdateCommand, cancellationToken);
            }

            return await _context.SaveChangesAsync(cancellationToken);
        }

        return new OperationStatus();
    }
}
