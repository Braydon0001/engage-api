namespace Engage.Application.Services.DCStockOnHands.Queries;

public class DCStockOnHandListQuery : IRequest<List<DCStockOnHandDto>>
{

}

public record DCStockOnHandListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<DCStockOnHandListQuery, List<DCStockOnHandDto>>
{
    public async Task<List<DCStockOnHandDto>> Handle(DCStockOnHandListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.DCStockOnHands.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.DCStockOnHandId)
                              .ProjectTo<DCStockOnHandDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}