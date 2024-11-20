// auto-generated
namespace Engage.Application.Services.ProductTransactionStatuses.Queries;

public class ProductTransactionStatusOptionListQuery : IRequest<List<ProductTransactionStatusOption>>
{ 

}

public class ProductTransactionStatusOptionListHandler : ListQueryHandler, IRequestHandler<ProductTransactionStatusOptionListQuery, List<ProductTransactionStatusOption>>
{
    public ProductTransactionStatusOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductTransactionStatusOption>> Handle(ProductTransactionStatusOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductTransactionStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<ProductTransactionStatusOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}