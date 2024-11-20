using Engage.Application.Services.ClaimHistories.Models;

namespace Engage.Application.Services.ClaimHistories.Queries;

public class ClaimHistoriesQuery : GetQuery, IRequest<ListResult<ClaimHistoryDto>>
{
    public int ClaimId { get; set; }
}

public class ClaimHistoriesQueryHandler : BaseQueryHandler, IRequestHandler<ClaimHistoriesQuery, ListResult<ClaimHistoryDto>>
{
    public ClaimHistoriesQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<ClaimHistoryDto>> Handle(ClaimHistoriesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.ClaimHistories.Where(e => e.ClaimId == request.ClaimId)
                                                    .OrderBy(e => e.ClaimHistoryId)
                                                    .ProjectTo<ClaimHistoryDto>(_mapper.ConfigurationProvider)
                                                    .ToListAsync(cancellationToken);

        return new ListResult<ClaimHistoryDto>(entities);
    }
}
