using Engage.Application.Services.EngageRegionClaimManagers.Models;

namespace Engage.Application.Services.EngageRegionClaimManagers.Queries
{
    public class EngageRegionClaimManagersQuery: IRequest<ListResult<EngageRegionClaimManagerDto>>
    {
        public int? EngageRegionId { get; set; }
        public int? ClaimAccountManagerId { get; set; }
    }

    public class EngageRegionClaimManagersQueryHandler : BaseQueryHandler, IRequestHandler<EngageRegionClaimManagersQuery, ListResult<EngageRegionClaimManagerDto>>
    {
        public EngageRegionClaimManagersQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ListResult<EngageRegionClaimManagerDto>> Handle(EngageRegionClaimManagersQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.EngageRegionClaimManagers.AsQueryable();

            if (request.EngageRegionId.HasValue)
            {
                queryable = queryable.Where(e => e.EngageRegionId == request.EngageRegionId);
            }
            
            if (request.ClaimAccountManagerId.HasValue)
            {
                queryable = queryable.Where(e => e.UserId == request.ClaimAccountManagerId);
            }

            var entities = await queryable.OrderBy(e => e.EngageRegion.Name)
                                          .ThenBy(e => e.User.FullName)
                                          .ProjectTo<EngageRegionClaimManagerDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);

            return new ListResult<EngageRegionClaimManagerDto>(entities);
        }
    }
}
