namespace Engage.Application.Services.Creditors.Commands;

public class BatchUpdateCreditorStatusCommand : IRequest<OperationStatus>
{
    public List<int> CreditorIds { get; set; }
    public int? CreditorStatusId { get; set; }
    public string Reason { get; set; }
}

public class BatchUpdateCreditorStatusCommandHandler : IRequestHandler<BatchUpdateCreditorStatusCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;

    public BatchUpdateCreditorStatusCommandHandler(IAppDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<OperationStatus> Handle(BatchUpdateCreditorStatusCommand command, CancellationToken cancellationToken)
    {
        foreach (var id in command.CreditorIds)
        {
            var creditor = await _context.Creditors.SingleAsync(x => x.CreditorId == id, cancellationToken);

            var updateStatusCommand = new UpdateCreditorStatusCommand
            {
                Id = creditor.CreditorId,
                Reason = command.Reason,
                SaveChanges = false,
            };

            if (command.CreditorStatusId.HasValue && command.CreditorStatusId == (int)CreditorStatusId.Rejected)
            {
                updateStatusCommand.CreditorStatusId = (int)CreditorStatusId.Rejected;
            }
            else
            {

                if (creditor.CreditorStatusId == (int)CreditorStatusId.New)
                {
                    updateStatusCommand.CreditorStatusId = (int)CreditorStatusId.RegionalApproved;
                }

                if (creditor.CreditorStatusId == (int)CreditorStatusId.RegionalApproved)
                {
                    updateStatusCommand.CreditorStatusId = (int)CreditorStatusId.Checked;
                }

                if (creditor.CreditorStatusId == (int)CreditorStatusId.Checked)
                {
                    updateStatusCommand.CreditorStatusId = (int)CreditorStatusId.Approved;
                }
            }

            await _mediator.Send(updateStatusCommand, cancellationToken);
        }

        return await _context.SaveChangesAsync(cancellationToken);
    }
}
