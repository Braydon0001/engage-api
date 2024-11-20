namespace Engage.Application.Services.CommunicationHistoryClaimFloats.Queries;

public class CommunicationHistoryClaimFloatListQuery : IRequest<List<CommunicationHistoryClaimFloatDto>>
{
    public int? ClaimFloatId { get; set; }
}

public record CommunicationHistoryClaimFloatListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CommunicationHistoryClaimFloatListQuery, List<CommunicationHistoryClaimFloatDto>>
{
    public async Task<List<CommunicationHistoryClaimFloatDto>> Handle(CommunicationHistoryClaimFloatListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CommunicationHistoryClaimFloats.AsQueryable().AsNoTracking();

        if (query.ClaimFloatId.HasValue)
        {
            queryable = queryable.Where(e => e.ClaimFloatId == query.ClaimFloatId.Value);
        }

        return await queryable.OrderBy(e => e.CommunicationHistoryId)
                              .ProjectTo<CommunicationHistoryClaimFloatDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}