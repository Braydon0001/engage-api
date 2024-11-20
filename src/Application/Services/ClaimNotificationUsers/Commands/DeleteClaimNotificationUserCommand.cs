namespace Engage.Application.Services.ClaimNotificationUsers.Commands;

public class DeleteClaimNotificationUserCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class DeleteClaimNotificationUserCommandHandler : IRequestHandler<DeleteClaimNotificationUserCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public DeleteClaimNotificationUserCommandHandler(IAppDbContext context)
    {
        _context = context;
    }
    public async Task<OperationStatus> Handle(DeleteClaimNotificationUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ClaimNotificationUsers.SingleAsync(s => s.ClaimNotificationUserId == request.Id, cancellationToken);

        _context.ClaimNotificationUsers.Remove(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = request.Id;

        return opStatus;
    }
}
