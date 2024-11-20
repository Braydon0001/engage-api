namespace Engage.Application.Services.CommunicationHistoryClaims.Queries;

public class CommunicationHistoryClaimListQuery : IRequest<List<CommunicationHistoryClaimDto>>
{
    public int? ClaimId { get; set; }
}

public record CommunicationHistoryClaimListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CommunicationHistoryClaimListQuery, List<CommunicationHistoryClaimDto>>
{
    public async Task<List<CommunicationHistoryClaimDto>> Handle(CommunicationHistoryClaimListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CommunicationHistoryClaims.AsQueryable().AsNoTracking();

        if (query.ClaimId.HasValue)
        {
            queryable = queryable.Where(e => e.ClaimId == query.ClaimId.Value);
        }

        return await queryable.OrderBy(e => e.CommunicationHistoryId)
                              .ProjectTo<CommunicationHistoryClaimDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}