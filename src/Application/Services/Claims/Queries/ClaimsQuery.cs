using Engage.Application.Services.Claims.Models;

namespace Engage.Application.Services.Claims.Queries;

public class ClaimsQuery : IRequest<ListResult<ClaimSubTotalDto>>
{
    public int? ClaimStatusId { get; set; }
    public int? ClaimSupplierStatusId { get; set; }
    public int? ClaimClassificationId { get; set; }
    public int? EngageRegionId { get; set; }
    public int? ClaimPeriodId { get; set; }
}

public class ClaimsQueryHandler : BaseQueryHandler, IRequestHandler<ClaimsQuery, ListResult<ClaimSubTotalDto>>
{
    private readonly IUserService _user;

    public ClaimsQueryHandler(IAppDbContext context, IMapper mapper, IUserService user) : base(context, mapper)
    {
        _user = user;
    }

    public async Task<ListResult<ClaimSubTotalDto>> Handle(ClaimsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.Claims.AsQueryable();

        if (request.ClaimStatusId.HasValue) { queryable = queryable.Where(e => e.ClaimStatusId == request.ClaimStatusId.Value); }
        if (request.ClaimSupplierStatusId.HasValue) { queryable = queryable.Where(e => e.ClaimSupplierStatusId == request.ClaimSupplierStatusId.Value); }
        if (request.ClaimClassificationId.HasValue) { queryable = queryable.Where(e => e.ClaimClassificationId == request.ClaimClassificationId.Value); }
        if (request.EngageRegionId.HasValue) { queryable = queryable.Where(e => e.Store.EngageRegionId == request.EngageRegionId.Value); }

        if (request.ClaimPeriodId.HasValue)
        {
            var claimPeriod = await _context.ClaimPeriods.Include(c => c.ClaimYear)
                                                     .SingleOrDefaultAsync(c => c.ClaimPeriodId == request.ClaimPeriodId.Value, cancellationToken);

            queryable = queryable
                            .Where(e => e.ClaimStatusId == (int)ClaimStatusId.Unapproved ?
                                    (e.UnapprovedDate.Value.Date >= claimPeriod.StartDate.Date
                                    && e.UnapprovedDate.Value.Date <= claimPeriod.EndDate.Date)
                                : (e.ApprovedDate.Value.Date >= claimPeriod.StartDate.Date
                                    && e.ApprovedDate.Value.Date <= claimPeriod.EndDate.Date)
                                );
        }

        if (!_user.IsHostSupplier)
        {
            var userId = await _user.GetUserIdAsync();
            queryable = queryable.Where(e => e.ClaimAccountManager.UserId == userId && e.ClaimClassification.IsSupplierProcess == true);

            //Get Previous Period
            var lastMonthDate = DateTime.Now.AddMonths(-1);
            var previousPeriod = await _context.ClaimPeriods.Where(e => lastMonthDate.Date >= e.StartDate.Date && lastMonthDate.Date <= e.EndDate.Date)
                                                                     .SingleOrDefaultAsync(cancellationToken);
            //Only return claims from previous period
            if (previousPeriod != null)
            {
                queryable = queryable
                                .Where(e => e.ApprovedDate.Value.Date >= previousPeriod.StartDate.Date
                                        && e.ApprovedDate.Value.Date <= previousPeriod.EndDate.Date);
            }
        }

        var entities = await queryable.IgnoreQueryFilters().OrderByDescending(e => e.ClaimId)
                                      .ProjectTo<ClaimSubTotalDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new ListResult<ClaimSubTotalDto>(entities);
    }
}
