using Engage.Application.Services.PaymentNotificationStatusUsers.Models;

namespace Engage.Application.Services.PaymentNotificationStatusUsers.Queries;

public class PaymentNotificationStatusUsersQuery : GetQuery, IRequest<ListResult<PaymentNotificationStatusUserDto>>
{
    public int? PaymentStatusId { get; set; }
    public int? UserId { get; set; }
    public int? EngageRegionId { get; set; }
}

public class PaymentNotificationStatusUserQueryHandler : BaseQueryHandler, IRequestHandler<PaymentNotificationStatusUsersQuery, ListResult<PaymentNotificationStatusUserDto>>
{
    public PaymentNotificationStatusUserQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<PaymentNotificationStatusUserDto>> Handle(PaymentNotificationStatusUsersQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.PaymentNotificationStatusUsers.AsQueryable();

        if (request.PaymentStatusId.HasValue)
        {
            queryable = queryable.Where(e => e.PaymentStatusId == request.PaymentStatusId);
        }

        if (request.UserId.HasValue)
        {
            queryable = queryable.Where(e => e.UserId == request.UserId);
        }

        if (request.EngageRegionId.HasValue)
        {
            queryable = queryable.Where(e => e.EngageRegionId == request.EngageRegionId);
        }

        var entities = await queryable.OrderBy(e => e.PaymentNotificationStatusUserId)
                                      .ProjectTo<PaymentNotificationStatusUserDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new ListResult<PaymentNotificationStatusUserDto>(entities);
    }
}
