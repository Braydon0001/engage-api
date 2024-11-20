namespace Engage.Application.Services.VoucherDetails.Commands;

public class BatchProcessVoucherDetailCommand : IRequest<OperationStatus>
{
    public List<int> VoucherDetailIds { get; set; }
}

public class BatchProcessVoucherDetailCommandHandler : IRequestHandler<BatchProcessVoucherDetailCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;

    public BatchProcessVoucherDetailCommandHandler(IAppDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<OperationStatus> Handle(BatchProcessVoucherDetailCommand command, CancellationToken cancellationToken)
    {
        foreach (var id in command.VoucherDetailIds)
        {
            var processVoucherDetailCommand = new ProcessVoucherDetailCommand
            {
                Id = id,
                SaveChanges = false
            };
            await _mediator.Send(processVoucherDetailCommand, cancellationToken);
        }

        return await _context.SaveChangesAsync(cancellationToken);
    }
}
