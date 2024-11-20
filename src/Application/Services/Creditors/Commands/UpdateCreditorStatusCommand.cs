using Engage.Application.Services.CreditorStatusHistories.Commands;

namespace Engage.Application.Services.Creditors.Commands;

public class UpdateCreditorStatusCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public int CreditorStatusId { get; set; }
    public string Reason { get; set; }
    public bool SaveChanges { get; set; } = true;
}

public class UpdateCreditorStatusCommandHandler : IRequestHandler<UpdateCreditorStatusCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;

    public UpdateCreditorStatusCommandHandler(IAppDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }
    public async Task<OperationStatus> Handle(UpdateCreditorStatusCommand command, CancellationToken cancellationToken)
    {
        var creditor = await _context.Creditors.SingleAsync(x => x.CreditorId == command.Id, cancellationToken);
        creditor.CreditorStatusId = command.CreditorStatusId;

        var updateStatusCommand = new CreditorStatusHistoryInsertCommand
        {
            CreditorId = command.Id,
            CreditorStatusId = command.CreditorStatusId,
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
