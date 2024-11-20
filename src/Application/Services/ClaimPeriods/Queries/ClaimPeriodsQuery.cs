using Engage.Application.Services.ClaimPeriods.Models;

namespace Engage.Application.Services.ClaimPeriods.Queries;

public class ClaimPeriodsQuery : GetQuery, IRequest<ListResult<ClaimPeriodDto>>
{
    public int? ClaimYearId { get; set; }
}

public class GetClaimPeriodsQueryHandler : BaseQueryHandler, IRequestHandler<ClaimPeriodsQuery, ListResult<ClaimPeriodDto>>
{
    public GetClaimPeriodsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<ListResult<ClaimPeriodDto>> Handle(ClaimPeriodsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.ClaimPeriods.AsQueryable();

        if (request.ClaimYearId.HasValue)
        {
            query = query.Where(e => e.ClaimYearId == request.ClaimYearId);
        }

        var entities = await query.OrderBy(e => e.ClaimPeriodId)
                                  .ProjectTo<ClaimPeriodDto>(_mapper.ConfigurationProvider)
                                  .ToListAsync(cancellationToken);

        return new ListResult<ClaimPeriodDto>(entities);
    }
}
