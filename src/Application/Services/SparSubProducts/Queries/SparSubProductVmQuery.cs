namespace Engage.Application.Services.SparSubProducts.Queries;

public record SparSubProductVmQuery(int Id) : IRequest<SparSubProductVm>;

public record SparSubProductVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparSubProductVmQuery, SparSubProductVm>
{
    public async Task<SparSubProductVm> Handle(SparSubProductVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SparSubProducts.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.SparProduct)
                             .Include(e => e.SparSubProductStatus)
                             .Include(e => e.SparSource)
                             .Include(e => e.DistributionCenter);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.SparSubProductId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<SparSubProductVm>(entity);
    }
}