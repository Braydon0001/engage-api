namespace Engage.Application.Services.EvoLedgers.Queries;

public class EvoLedgerListQuery : IRequest<List<EvoLedgerDto>>
{

}

public record EvoLedgerListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EvoLedgerListQuery, List<EvoLedgerDto>>
{
    public async Task<List<EvoLedgerDto>> Handle(EvoLedgerListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.EvoLedgers.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.EvoLedgerId)
                              .ProjectTo<EvoLedgerDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}