using Engage.Application.Services.ClaimEmailHistories.Models;

namespace Engage.Application.Services.ClaimEmailHistories.Queries;

public class ClaimEmailHistoryQuery : IRequest<ListResult<ClaimEmailHistoryDto>>
{
    public int? ClaimPeriodId { get; set; }
    public int? EmailTemplateId { get; set; }
}

public class ClaimEmailHistoryQueryHandler : BaseQueryHandler, IRequestHandler<ClaimEmailHistoryQuery, ListResult<ClaimEmailHistoryDto>>
{
    private readonly IUserService _user;

    public ClaimEmailHistoryQueryHandler(IAppDbContext context, IMapper mapper, IUserService user) : base(context, mapper)
    {
        _user = user;
    }

    public async Task<ListResult<ClaimEmailHistoryDto>> Handle(ClaimEmailHistoryQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.EmailHistories.AsQueryable();

        if (request.ClaimPeriodId.HasValue)
        {
            var claimPeriod = await _context.ClaimPeriods.Include(c => c.ClaimYear)
                                                     .SingleOrDefaultAsync(c => c.ClaimPeriodId == request.ClaimPeriodId.Value, cancellationToken);

            queryable = queryable
                                .Where(e => (e.Created.Date >= claimPeriod.StartDate.Date
                                    && e.Created.Date <= claimPeriod.EndDate.Date));
        }

        if (request.EmailTemplateId.HasValue)
        {
            queryable = queryable.Where(e => e.EmailTemplateId == request.EmailTemplateId);
        }

        var entities = await queryable.OrderByDescending(e => e.EmailHistoryId)
                                      .ProjectTo<ClaimEmailHistoryDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new ListResult<ClaimEmailHistoryDto>(entities);
    }
}