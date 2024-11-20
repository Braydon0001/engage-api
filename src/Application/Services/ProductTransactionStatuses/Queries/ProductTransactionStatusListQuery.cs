// auto-generated
namespace Engage.Application.Services.ProductTransactionStatuses.Queries;

public class ProductTransactionStatusListQuery : IRequest<List<ProductTransactionStatusDto>>
{

}

public class ProductTransactionStatusListHandler : ListQueryHandler, IRequestHandler<ProductTransactionStatusListQuery, List<ProductTransactionStatusDto>>
{
    public ProductTransactionStatusListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductTransactionStatusDto>> Handle(ProductTransactionStatusListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductTransactionStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<ProductTransactionStatusDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}