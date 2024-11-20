namespace Engage.Application.Services.CreditorNotificationStatusUsers.Commands;

public class DeleteCreditorNotificationStatusUserCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class DeleteCreditorNotificationStatusUserCommandHandler : IRequestHandler<DeleteCreditorNotificationStatusUserCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public DeleteCreditorNotificationStatusUserCommandHandler(IAppDbContext context)
    {
        _context = context;
    }
    public async Task<OperationStatus> Handle(DeleteCreditorNotificationStatusUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.CreditorNotificationStatusUsers.SingleAsync(s => s.CreditorNotificationStatusUserId == request.Id, cancellationToken);

        _context.CreditorNotificationStatusUsers.Remove(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = request.Id;

        return opStatus;
    }
}
