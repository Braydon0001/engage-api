namespace Engage.Application.Services.PaymentNotificationStatusUsers.Commands;

public class BatchCreatePaymentNotificationStatusUsersCommand : IRequest<OperationStatus>
{
    public List<int> PaymentStatusIds { get; set; }
    public List<int> UserIds { get; set; }
    public List<int> EngageRegionIds { get; set; }
}

public class BatchCreatePaymentNotificationStatusUsersCommandHandler(IAppDbContext context, IMapper mapper) : BaseCreateCommandHandler(context, mapper), IRequestHandler<BatchCreatePaymentNotificationStatusUsersCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(BatchCreatePaymentNotificationStatusUsersCommand request, CancellationToken cancellationToken)
    {
        foreach (var paymentStatusId in request.PaymentStatusIds)
        {
            foreach (var userId in request.UserIds)
            {
                foreach (var engageRegionId in request.EngageRegionIds)
                {
                    var exists = await _context.PaymentNotificationStatusUsers.AnyAsync(x => x.PaymentStatusId == paymentStatusId && x.UserId == userId && x.EngageRegionId == engageRegionId,
                                                                                                           cancellationToken);
                    if (!exists)
                    {
                        var entity = new PaymentNotificationStatusUser
                        {
                            PaymentStatusId = paymentStatusId,
                            EngageRegionId = engageRegionId,
                            UserId = userId,
                        };

                        _context.PaymentNotificationStatusUsers.Add(entity);
                    }
                }
            }
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        return opStatus;
    }
}
