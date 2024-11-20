namespace Engage.Application.Services.ClaimNotificationUsers.Commands;

public class BatchCreateClaimNotificationUsersCommand : IRequest<OperationStatus>
{
    public List<int> ClaimStatusIds { get; set; }
    public List<int> UserIds { get; set; }
    public List<int> EngageRegionIds { get; set; }
}

public class BatchCreateClaimNotificationUsersCommandHandler : BaseCreateCommandHandler, IRequestHandler<BatchCreateClaimNotificationUsersCommand, OperationStatus>
{
    public BatchCreateClaimNotificationUsersCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(BatchCreateClaimNotificationUsersCommand request, CancellationToken cancellationToken)
    {
        foreach (var claimStatusId in request.ClaimStatusIds)
        {
            foreach (var userId in request.UserIds)
            {
                foreach (var engageRegionId in request.EngageRegionIds)
                {
                    var exists = await _context.ClaimNotificationUsers.AnyAsync(x => x.ClaimStatusId == claimStatusId && x.UserId == userId && x.EngageRegionId == engageRegionId,
                                                                                                           cancellationToken);
                    if (!exists)
                    {
                        var entity = new ClaimNotificationUser
                        {
                            ClaimStatusId = claimStatusId,
                            EngageRegionId = engageRegionId,
                            UserId = userId,
                        };

                        _context.ClaimNotificationUsers.Add(entity);
                    }
                }
            }
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        return opStatus;
    }
}
