namespace Engage.Application.Services.ProductOrderTypes.Queries;

public record ProductOrderTypeVmQuery(int Id) : IRequest<ProductOrderTypeVm>;

public record ProductOrderTypeVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderTypeVmQuery, ProductOrderTypeVm>
{
    public async Task<ProductOrderTypeVm> Handle(ProductOrderTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProductOrderTypes.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProductOrderTypeId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<ProductOrderTypeVm>(entity);
    }
}