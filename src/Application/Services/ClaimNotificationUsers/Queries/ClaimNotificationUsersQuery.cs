using Engage.Application.Services.ClaimNotificationUsers.Models;

namespace Engage.Application.Services.ClaimNotificationUsers.Queries;

public class ClaimNotificationUsersQuery : GetQuery, IRequest<ListResult<ClaimNotificationUserDto>>
{
    public int? ClaimStatusId { get; set; }
    public int? UserId { get; set; }
    public int? EngageRegionId { get; set; }
}

public class ClaimNotificationUserQueryHandler : BaseQueryHandler, IRequestHandler<ClaimNotificationUsersQuery, ListResult<ClaimNotificationUserDto>>
{
    public ClaimNotificationUserQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<ClaimNotificationUserDto>> Handle(ClaimNotificationUsersQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.ClaimNotificationUsers.AsQueryable();

        if (request.ClaimStatusId.HasValue)
        {
            queryable = queryable.Where(e => e.ClaimStatusId == request.ClaimStatusId);
        }

        if (request.UserId.HasValue)
        {
            queryable = queryable.Where(e => e.UserId == request.UserId);
        }

        if (request.EngageRegionId.HasValue)
        {
            queryable = queryable.Where(e => e.EngageRegionId == request.EngageRegionId);
        }

        var entities = await queryable.OrderBy(e => e.ClaimNotificationUserId)
                                      .ProjectTo<ClaimNotificationUserDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new ListResult<ClaimNotificationUserDto>(entities);
    }
}
