namespace Engage.Application.Services.ClaimNotificationUsers.Commands;

public class CreateClaimNotificationUserCommand : ClaimNotificationUserCommand, IRequest<OperationStatus>
{
}

public class CreateClaimNotificationUserCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateClaimNotificationUserCommand, OperationStatus>
{
    public CreateClaimNotificationUserCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(CreateClaimNotificationUserCommand request, CancellationToken cancellationToken)
    {
        var entity = new ClaimNotificationUser
        {
            ClaimStatusId = request.ClaimStatusId,
            EngageRegionId = request.EngageRegionId,
            UserId = request.UserId,
        };

        _context.ClaimNotificationUsers.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.ClaimNotificationUserId;
        return opStatus;
    }
}
