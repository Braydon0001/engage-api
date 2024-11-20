namespace Engage.Application.Services.CreditorNotificationStatusUsers.Commands;

public class BatchCreateCreditorNotificationStatusUsersCommand : IRequest<OperationStatus>
{
    public List<int> CreditorStatusIds { get; set; }
    public List<int> UserIds { get; set; }
    //public List<int> EngageRegionIds { get; set; }
}

public class BatchCreateCreditorNotificationStatusUsersCommandHandler(IAppDbContext context, IMapper mapper) : BaseCreateCommandHandler(context, mapper), IRequestHandler<BatchCreateCreditorNotificationStatusUsersCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(BatchCreateCreditorNotificationStatusUsersCommand request, CancellationToken cancellationToken)
    {
        foreach (var creditorStatusId in request.CreditorStatusIds)
        {
            foreach (var userId in request.UserIds)
            {
                //foreach (var engageRegionId in request.EngageRegionIds)
                //{
                var exists = await _context.CreditorNotificationStatusUsers.AnyAsync(x => x.CreditorStatusId == creditorStatusId && x.UserId == userId //&& x.EngageRegionId == engageRegionId,
                                                                                                       , cancellationToken);
                if (!exists)
                {
                    var entity = new CreditorNotificationStatusUser
                    {
                        CreditorStatusId = creditorStatusId,
                        //EngageRegionId = engageRegionId,
                        UserId = userId,
                    };

                    _context.CreditorNotificationStatusUsers.Add(entity);
                }
                //}
            }
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        return opStatus;
    }
}
