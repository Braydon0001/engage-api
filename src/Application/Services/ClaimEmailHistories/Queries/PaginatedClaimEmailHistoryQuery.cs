using Engage.Application.Services.ClaimEmailHistories.Models;
using Finbuckle.MultiTenant.Abstractions;

namespace Engage.Application.Services.ClaimEmailHistories.Queries
{
    public class PaginatedClaimEmailHistoryQuery : GetQuery, IRequest<PaginatedListResult<ClaimEmailHistoryDto>>
    {
        public int? ClaimPeriodId { get; set; }
        public int? EmailTemplateId { get; set; }
    }

    public class PaginatedClaimEmailHistoryQueryHandler : BaseQueryHandler, IRequestHandler<PaginatedClaimEmailHistoryQuery, PaginatedListResult<ClaimEmailHistoryDto>>
    {
        private readonly IMultiTenantContextAccessor _multiTenantContextAccessor;
        public PaginatedClaimEmailHistoryQueryHandler(IAppDbContext context, IMapper mapper, IMultiTenantContextAccessor multiTenantContextAccessor) : base(context, mapper)
        {
            _multiTenantContextAccessor = multiTenantContextAccessor;
        }
        public async Task<PaginatedListResult<ClaimEmailHistoryDto>> Handle(PaginatedClaimEmailHistoryQuery request, CancellationToken cancellationToken)
        {
            var query = _context.EmailHistories.AsQueryable();

            if (request.ClaimPeriodId.HasValue)
            {
                var claimPeriod = await _context.ClaimPeriods.Where(c => c.ClaimPeriodId == request.ClaimPeriodId.Value)
                                                         .FirstOrDefaultAsync(cancellationToken);

                if (claimPeriod != null)
                {
                    query = query
                                .Where(e => (e.Created.Date >= claimPeriod.StartDate.Date
                                            && e.Created.Date <= claimPeriod.EndDate.Date));
                }
            }

            if (request.EmailTemplateId.HasValue)
            {
                query = query.Where(e => e.EmailTemplateId == request.EmailTemplateId);
            }

            var (queryable, paginationResult) = query.Paginate(request, _multiTenantContextAccessor);

            var entities = await queryable.ProjectTo<ClaimEmailHistoryDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);

            return new PaginatedListResult<ClaimEmailHistoryDto>(entities, paginationResult);
        }
    }
}
