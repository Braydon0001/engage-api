using Engage.Application.Services.CreditorNotificationStatusUsers.Models;

namespace Engage.Application.Services.CreditorNotificationStatusUsers.Queries;

public class CreditorNotificationStatusUsersQuery : GetQuery, IRequest<ListResult<CreditorNotificationStatusUserDto>>
{
    public int? CreditorStatusId { get; set; }
    public int? UserId { get; set; }
    public int? EngageRegionId { get; set; }
}

public class CreditorNotificationStatusUserQueryHandler : BaseQueryHandler, IRequestHandler<CreditorNotificationStatusUsersQuery, ListResult<CreditorNotificationStatusUserDto>>
{
    public CreditorNotificationStatusUserQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<CreditorNotificationStatusUserDto>> Handle(CreditorNotificationStatusUsersQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.CreditorNotificationStatusUsers.AsQueryable();

        if (request.CreditorStatusId.HasValue)
        {
            queryable = queryable.Where(e => e.CreditorStatusId == request.CreditorStatusId);
        }

        if (request.UserId.HasValue)
        {
            queryable = queryable.Where(e => e.UserId == request.UserId);
        }

        if (request.EngageRegionId.HasValue)
        {
            queryable = queryable.Where(e => e.EngageRegionId == request.EngageRegionId);
        }

        var entities = await queryable.OrderBy(e => e.CreditorNotificationStatusUserId)
                                      .ProjectTo<CreditorNotificationStatusUserDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new ListResult<CreditorNotificationStatusUserDto>(entities);
    }
}
