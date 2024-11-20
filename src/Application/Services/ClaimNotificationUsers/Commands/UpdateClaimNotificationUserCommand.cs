namespace Engage.Application.Services.ClaimNotificationUsers.Commands;

public class UpdateClaimNotificationUserCommand : ClaimNotificationUserCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateClaimNotificationUserCommandHandler : BaseCreateCommandHandler, IRequestHandler<UpdateClaimNotificationUserCommand, OperationStatus>
{
    public UpdateClaimNotificationUserCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(UpdateClaimNotificationUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ClaimNotificationUsers.SingleAsync(x => x.ClaimNotificationUserId == request.Id, cancellationToken);

        _mapper.Map(request, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.ClaimNotificationUserId;
        return opStatus;
    }
}
