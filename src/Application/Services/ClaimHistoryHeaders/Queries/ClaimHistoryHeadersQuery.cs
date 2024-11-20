using Engage.Application.Services.ClaimBatches.Models;

namespace Engage.Application.Services.ClaimBatches.Queries;

public class ClaimHistoryHeadersQuery : GetQuery, IRequest<ListResult<ClaimHistoryHeaderDto>>
{
}

public class ClaimHistoryHeadersQueryHandler : BaseQueryHandler, IRequestHandler<ClaimHistoryHeadersQuery, ListResult<ClaimHistoryHeaderDto>>
{
    public ClaimHistoryHeadersQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<ClaimHistoryHeaderDto>> Handle(ClaimHistoryHeadersQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.ClaimHistoryHeaders.OrderBy(e => e.ClaimHistoryHeaderId)
                                                         .ProjectTo<ClaimHistoryHeaderDto>(_mapper.ConfigurationProvider)
                                                         .ToListAsync(cancellationToken);

        return new ListResult<ClaimHistoryHeaderDto>(entities);

    }
}
