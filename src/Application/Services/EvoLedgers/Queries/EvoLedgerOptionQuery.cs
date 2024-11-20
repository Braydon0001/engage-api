namespace Engage.Application.Services.EvoLedgers.Queries;

public class EvoLedgerOptionQuery : IRequest<List<EvoLedgerOption>>
{ 

}

public record EvoLedgerOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EvoLedgerOptionQuery, List<EvoLedgerOption>>
{
    public async Task<List<EvoLedgerOption>> Handle(EvoLedgerOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.EvoLedgers.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.EvoLedgerId)
                              .ProjectTo<EvoLedgerOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}