using Engage.Application.Services.ClaimEmails.Models;

namespace Engage.Application.Services.Claims.Queries;

public class ClaimAccountManagersToRemindQuery : IRequest<ListResult<ClaimAccountManagersToRemindDto>>
{
    public int? ClaimPeriodId { get; set; }
}

public class ClaimAccountManagersToRemindQueryHandler : BaseQueryHandler, IRequestHandler<ClaimAccountManagersToRemindQuery, ListResult<ClaimAccountManagersToRemindDto>>
{
    private readonly IUserService _user;

    public ClaimAccountManagersToRemindQueryHandler(IAppDbContext context, IMapper mapper, IUserService user) : base(context, mapper)
    {
        _user = user;
    }

    public async Task<ListResult<ClaimAccountManagersToRemindDto>> Handle(ClaimAccountManagersToRemindQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierClaimAccountManagers.Include(e => e.User).AsQueryable();

        var claimsQuaryable = _context.Claims
                                    .Include(c => c.ClaimAccountManager)
                                    .Where(e => e.ClaimAccountManagerId > 0
                                        && e.ClaimSupplierStatusId != (int)ClaimSupplierStatusId.Approved
                                        && e.ClaimStatusId == (int)ClaimStatusId.Approved);

        if (request.ClaimPeriodId.HasValue)
        {
            var claimPeriod = await _context.ClaimPeriods.Include(c => c.ClaimYear)
                                                     .SingleOrDefaultAsync(c => c.ClaimPeriodId == request.ClaimPeriodId.Value, cancellationToken);

            claimsQuaryable = claimsQuaryable
                                .Where(e => (e.ApprovedDate.Value.Date >= claimPeriod.StartDate.Date
                                    && e.ApprovedDate.Value.Date <= claimPeriod.EndDate.Date));
        }

        var claimManagerIds = await claimsQuaryable
                                    .Select(e => e.ClaimAccountManagerId)
                                    .Distinct()
                                    .ToListAsync();

        queryable = queryable.Where(e => claimManagerIds.Contains(e.UserId));

        var entities = await queryable.OrderBy(e => e.User.FirstName)
                                      .ProjectTo<ClaimAccountManagersToRemindDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new ListResult<ClaimAccountManagersToRemindDto>(entities);
    }
}