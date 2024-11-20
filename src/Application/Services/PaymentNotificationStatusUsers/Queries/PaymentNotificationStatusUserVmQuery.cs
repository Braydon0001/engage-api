using Engage.Application.Services.PaymentNotificationStatusUsers.Models;

namespace Engage.Application.Services.PaymentNotificationStatusUsers.Queries;

public class PaymentNotificationStatusUserVmQuery : GetByIdQuery, IRequest<PaymentNotificationStatusUserVm>
{
}

public class PaymentNotificationStatusUserVmQueryHandler : BaseQueryHandler, IRequestHandler<PaymentNotificationStatusUserVmQuery, PaymentNotificationStatusUserVm>
{
    public PaymentNotificationStatusUserVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<PaymentNotificationStatusUserVm> Handle(PaymentNotificationStatusUserVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.PaymentNotificationStatusUsers.Include(e => e.User)
                                                          .Include(e => e.EngageRegion)
                                                          .Include(e => e.PaymentStatus)
                                                          .SingleAsync(e => e.PaymentNotificationStatusUserId == request.Id, cancellationToken);

        return _mapper.Map<PaymentNotificationStatusUser, PaymentNotificationStatusUserVm>(entity);
    }
}
