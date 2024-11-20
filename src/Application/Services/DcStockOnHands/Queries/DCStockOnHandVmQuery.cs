namespace Engage.Application.Services.DCStockOnHands.Queries;

public record DCStockOnHandVmQuery(int Id) : IRequest<DCStockOnHandVm>;

public record DCStockOnHandVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<DCStockOnHandVmQuery, DCStockOnHandVm>
{
    public async Task<DCStockOnHandVm> Handle(DCStockOnHandVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.DCStockOnHands.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.DCProduct);

        var entity = await queryable.SingleOrDefaultAsync(e => e.DCStockOnHandId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<DCStockOnHandVm>(entity);
    }
}